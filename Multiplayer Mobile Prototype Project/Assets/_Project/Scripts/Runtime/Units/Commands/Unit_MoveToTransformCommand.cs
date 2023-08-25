using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    using Motors;
    
    public class Unit_MoveToTransformCommand : Unit_Transform_Command
    {
        public override bool Execute(Transform obj1, Transform collectible)
        {
            var agent = _unit.GetAgent();

            agent.isStopped = false;
            
            Unit_LocomotionMotor.HandleMovement(agent, collectible);
            
            var isCompleted = Unit_LocomotionMotor.IsReachTheTarget(obj1, collectible, agent.speed / 2);

            if (isCompleted)
                agent.isStopped = true;
            
            _unit.GetAnimator().SetBool("Moving", !isCompleted);
            
            return isCompleted;
        }
    }
}