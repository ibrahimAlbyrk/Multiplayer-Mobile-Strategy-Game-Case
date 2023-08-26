using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    using Motors;
    using Animations;
    
    public class Unit_MoveToVectorCommand : Unit_Vector_Command
    {
        public override bool Execute(Transform obj1, VectorCommand target)
        {
            var agent = _unit.GetAgent();

            agent.isStopped = false;
            
            Unit_LocomotionMotor.HandleMovement(agent, target.Vector3);
            
            var isCompleted = Unit_LocomotionMotor.IsReachTheTarget(obj1.position, target.Vector3, agent.speed / 2);

            if (isCompleted)
                agent.isStopped = true;
            
            _unit.GetAnimator().SetBool(AnimationParams.MOVING_PARAM, !isCompleted);
            
            return isCompleted;
        }

        public override void End()
        {
            _unit.GetAnimator().SetBool(AnimationParams.MOVING_PARAM, false);
        }
    }
}