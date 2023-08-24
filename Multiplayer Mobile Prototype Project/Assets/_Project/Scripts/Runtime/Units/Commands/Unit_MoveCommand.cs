using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    using Motors;
    
    public class Unit_MoveCommand : Unit_Transform_Command
    {
        public override bool Execute(Transform obj1, Transform obj2)
        {
            var agent = _unit.GetAgent();

            agent.SetDestination(obj2.position);
            
            var isCompleted = Unit_MovementMotor.IsReachTheTarget(obj1, obj2, agent.speed);
            
            return isCompleted;
        }
    }
}