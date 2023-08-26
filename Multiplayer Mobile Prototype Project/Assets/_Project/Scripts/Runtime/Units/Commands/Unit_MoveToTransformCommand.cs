using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    using Motors;
    using Animations;
    
    public class Unit_MoveToTransformCommand : Unit_Transform_Command
    {
        public override bool Execute(Transform obj1, Transform target)
        {
            var agent = _unit.GetAgent();

            agent.isStopped = false;
            
            Unit_LocomotionMotor.HandleMovement(agent, target);
            
            var isCompleted = Unit_LocomotionMotor.IsReachTheTarget(obj1, target, agent.speed / 2) || target == null;

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