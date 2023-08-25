using UnityEngine;

namespace Core.Runtime.Collectible
{
    using Scriptable;
    using Units.Types;
    using Units.Managers;
    
    public class ResourceCollectible : MonoBehaviour, ICollectible
    {
        public Resource_SO Data;
        
        public int ResourceAmount = 30;
        
        public void Collect(bool isMultiple)
        {
            if (!isMultiple)
                UnitManager.Instance.ResetCommands();
            
            UnitManager.Instance.SendCommandToSelectedUnits(Unit_CommandType.MoveToTransform, transform);
            UnitManager.Instance.SendCommandToSelectedUnits(Unit_CommandType.ResourceCollect, this);
        }
    }
}