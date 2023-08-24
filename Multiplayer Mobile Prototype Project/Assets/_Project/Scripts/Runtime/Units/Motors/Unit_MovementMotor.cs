﻿using UnityEngine;
using UnityEngine.AI;

namespace Core.Runtime.Units.Motors
{
    public static class Unit_MovementMotor
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

        public static bool IsReachTheTarget(Transform t1, Transform t2, float threshold)
        {
            return Vector3.Distance(t1.position, t2.position) <= threshold;
        }
    }
}