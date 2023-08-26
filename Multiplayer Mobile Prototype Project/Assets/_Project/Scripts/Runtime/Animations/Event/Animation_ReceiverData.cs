using System;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Runtime.Animations.Event
{
    [Serializable]
    public class Animation_ReceiverData
    {
        public Animator Animator;
        public List<EventData> Events;

        public Animation_ReceiverData(Animator animator)
        {
            Animator = animator;
            Events = new List<EventData>();
        }
    }
}