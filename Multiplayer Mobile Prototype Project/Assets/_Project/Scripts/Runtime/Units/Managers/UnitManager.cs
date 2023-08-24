using System;
using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Core.Runtime.Units.Managers
{
    using Types;
    using Singleton;
    using Providers;
    using Responses;
    
    public class UnitManager : MonoBehaviorSingleton<UnitManager>
    {
        [SerializeField] private string _unitLayer;
        
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

            return true;
        }

        public bool RemoveUnit(Unit unit)
        {
            return _units.Remove(unit);
        }

        #endregion

        #region Spawn Methods

        public void SpawnUnit()
        {
            SpawnUnitWithColor(null);
        }

        public void SpawnUnitWithColor(Color? color)
        {
            var spawnedUnit = PhotonNetwork.Instantiate("Units/WorkerUnit",
                new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10)), Quaternion.identity);

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

        public void SendCommand(Unit unit, object commandObject, params Unit_CommandType[] commandTypes)
        {
            _response.SendCommand(unit, commandObject, commandTypes);
        }

        public void SendCommand(Unit unit, Unit_CommandType commandType, object commandObject)
        {
            _response.SendCommand(unit, commandType, commandObject);
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
            var providerType = Type.GetType("VR.Runtime.Assistant.Providers.AssistantRobot_" +
                                            $"{_commandProviderType}CommandProvider")!;

            _provider = (Activator.CreateInstance(providerType) as Units_CommandProvider)!;

            _provider?.Init(_units);
        }

        private void ResponseSetter()
        {
            var responseType = Type.GetType("VR.Runtime.Assistant.Responses.AssistantRobot_" +
                                            $"{_commandResponseType}CommandResponse")!;

            _response = (Activator.CreateInstance(responseType) as Units_CommandResponse)!;

            _response?.Init(_provider);
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