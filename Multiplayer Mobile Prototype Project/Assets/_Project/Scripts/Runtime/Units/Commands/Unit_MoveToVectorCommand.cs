using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    using Motors;
    
    public class Unit_MoveToVectorCommand : Unit_Vector_Command
    {
        public override bool Execute(Transform obj1, VectorCommand obj2)
        {
            var agent = _unit.GetAgent();

            agent.isStopped = false;
            
            Unit_LocomotionMotor.HandleMovement(agent, obj2.Vector3);
            
            var isCompleted = Unit_LocomotionMotor.IsReachTheTarget(obj1.position, obj2.Vector3, agent.speed / 2);

            if (isCompleted)
                agent.isStopped = true;
            
            return isCompleted;
        }
    }
}