using UnityEngine;
using UnityEngine.AI;

namespace Core.Runtime.Units.Motors
{
    public static class Unit_LocomotionMotor
    {
        /// <summary>
        /// Handles movement of the agent
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="target"></param>
        public static void HandleMovement(NavMeshAgent agent, Transform target)
        {
            if (target == null) return;
            
            agent.SetDestination(target.position);
        }
        
        public static void HandleMovement(NavMeshAgent agent, Vector3 pos)
        {
            agent.SetDestination(pos);
        }

        public static void LookTransform(Transform t1, Transform target)
        {
            var dir = (target.position - t1.position).normalized;

            var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            
            t1.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }

        public static bool IsReachTheTarget(Transform t1, Transform t2, float threshold)
        {
            if (t1 == null || t2 == null) return true;
            
            return Vector3.Distance(t1.position, t2.position) <= threshold;
        }
        
        public static bool IsReachTheTarget(Vector3 pos1, Vector3 pos2, float threshold)
        {
            return Vector3.Distance(pos1, pos2) <= threshold;
        }
    }
}