using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Runtime.Units.Managers
{
    using Singleton;
    
    public class UnitManager : MonoBehaviorSingleton<UnitManager>
    {
        [SerializeField] private string _unitLayer;
        
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
    }
}