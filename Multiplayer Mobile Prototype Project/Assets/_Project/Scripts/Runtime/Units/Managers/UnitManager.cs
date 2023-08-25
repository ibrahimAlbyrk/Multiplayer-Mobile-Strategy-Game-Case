using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Core.Runtime.Units.Managers
{
    using Types;
    using Singleton;
    using Providers;
    using Responses;
    using Selection.Managers;
    using NETWORK.Instantiate;
    
    public class UnitManager : MonoBehaviorSingleton<UnitManager>
    {
        [SerializeField] private string _unitLayer;

        [SerializeField] private GameObject _unitPrefab;
        
        [Header("Selectors")]
        [SerializeField] private Unit_CommandProviderType _commandProviderType;
        [SerializeField] private Unit_CommandResponseType _commandResponseType;
        
        private Units_CommandProvider _provider;
        private Units_CommandResponse _response;
        
        private readonly List<Unit> _units = new();
        
        #region Get Methods
        
        public IReadOnlyList<Unit> GetUnits() => _units;

        public string GetUnitLayer() => _unitLayer;
        
        #endregion

        #region Listener Methods

        public bool AddUnit(Unit unit)
        {
            if (_units.Contains(unit)) return false;
            
            _units.Add(unit);
            
            _provider.AddCommandHandler(unit);

            return true;
        }

        public bool RemoveUnit(Unit unit)
        {
            var isRemoved = _units.Remove(unit);
            
            if(isRemoved)
                _provider.RemoveCommandHandler(unit);

            return isRemoved;
        }

        #endregion

        #region Spawn Methods

        public void SpawnUnit()
        {
            SpawnUnitWithColor(null);
        }

        public void SpawnUnitWithColor(Color? color)
        {
            var spawnedUnit = NetworkInstantiater.Instantiate(_unitPrefab,
                new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10)),
                Quaternion.identity);

            spawnedUnit.layer = LayerMask.NameToLayer(_unitLayer);

            var unit = spawnedUnit.GetComponent<Unit>();

            if(color.HasValue)
                unit.SetColor(color.Value);
        }

        public void SpawnUnitsWithColor(Color color, int count)
        {
            for (var i = 0; i < count; i++)
                SpawnUnitWithColor(color);
        }

        #endregion
        
        #region Global Command Methods

        public void ResetCommands()
        {
            var selectedUnits = UnitSelectionManager.Instance.GetSelectedUnits();

            foreach (var selectedUnit in selectedUnits)
            {
                _provider.ResetCommands(selectedUnit);
            }
        }

        public void SendCommandToSelectedUnits(object commandObject, params Unit_CommandType[] commandTypes)
        {
            var selectedUnits = UnitSelectionManager.Instance.GetSelectedUnits();

            foreach (var selectedUnit in selectedUnits)
            {
                _response.SendCommand(selectedUnit, commandObject, commandTypes);   
            }
        }

        public void SendCommandToSelectedUnits(Unit_CommandType commandType, object commandObject)
        {
            var selectedUnits = UnitSelectionManager.Instance.GetSelectedUnits();

            foreach (var selectedUnit in selectedUnits)
            {
                _response.SendCommand(selectedUnit, commandType, commandObject);   
            }
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            ProviderSetter();
            ResponseSetter();
        }
        
        private void ProviderSetter()
        {
            var providerType = Type.GetType("Core.Runtime.Units.Providers.Units_" +
                                            $"{_commandProviderType}CommandProvider")!;

            if (providerType == null)
            {
                throw new ArgumentNullException($"UnitManager", "Provider Type is null");
            }
            
            _provider = (Activator.CreateInstance(providerType) as Units_CommandProvider)!;
        }
        
        private void ResponseSetter()
        {
            var responseType = Type.GetType("Core.Runtime.Units.Responses.Units_" +
                                            $"{_commandResponseType}CommandResponse")!;
            
            if (responseType == null)
                throw new ArgumentNullException($"UnitManager", "Response Type is null");

            _response = (Activator.CreateInstance(responseType) as Units_CommandResponse)!;

            _response.Init(_provider);
        }
        #endregion
        
        #region Base methods

        protected override void Awake()
        {
            base.Awake();
            
            Init();
        }

        private void Update() => _provider.RunCommands();

        #endregion
    }
}