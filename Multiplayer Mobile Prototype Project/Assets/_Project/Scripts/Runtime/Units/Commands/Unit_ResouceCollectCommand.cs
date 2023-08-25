using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    using Motors;
    using Collectible;
    
    public class Unit_ResourceCollectCommand : Unit_ResourceCollect_Command
    {
        private const float _CollectCountDown = .5f;
        private float _timer;
        
        public override bool Execute(Transform obj1, ResourceCollectible collectible)
        {
            Unit_LocomotionMotor.LookTransform(obj1, collectible.transform);
            
            if (_timer >= _CollectCountDown)
            {
                collectible.ResourceAmount--;
                _timer = 0f;
            }
            else
                _timer += Time.fixedDeltaTime;
            
            var isCompleted = collectible.ResourceAmount < 1;
            
            _unit.GetAnimator().SetBool("Working", !isCompleted);

            return isCompleted;
        }
    }
}