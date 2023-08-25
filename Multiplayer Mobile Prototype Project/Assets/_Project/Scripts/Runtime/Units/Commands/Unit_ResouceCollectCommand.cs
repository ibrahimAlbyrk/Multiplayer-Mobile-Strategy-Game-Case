using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    using Motors;
    using Collectible;
    
    public class Unit_ResourceCollectCommand : Unit_ResourceCollect_Command
    {
        private const float _CollectCountDown = .5f;
        private float _timer;
        
        public override bool Execute(Transform obj1, ResourceCollectible obj2)
        {
            Unit_LocomotionMotor.LookTransform(obj1, obj2.transform);
            
            if (_timer >= _CollectCountDown)
            {
                obj2.ResourceAmount--;
                _timer = 0f;
            }
            else
                _timer += Time.fixedDeltaTime;
            
            var isCompleted = obj2.ResourceAmount < 1;

            return isCompleted;
        }
    }
}