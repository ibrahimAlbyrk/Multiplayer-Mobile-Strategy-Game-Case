using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    using Motors;
    
    public class Unit_MoveToTransformCommand : Unit_Transform_Command
    {
        public override bool Execute(Transform obj1, Transform obj2)
        {
            var agent = _unit.GetAgent();

            agent.isStopped = false;
            
            Unit_LocomotionMotor.HandleMovement(agent, obj2);
            
            var isCompleted = Unit_LocomotionMotor.IsReachTheTarget(obj1, obj2, agent.speed / 2);

            if (isCompleted)
                agent.isStopped = true;
            
            return isCompleted;
        }
    }
}