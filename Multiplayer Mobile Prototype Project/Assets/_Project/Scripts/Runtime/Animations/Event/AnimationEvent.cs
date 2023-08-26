using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Runtime.Animations.Event
{
    public static class AnimationEvent
    {
        private static List<Animation_ReceiverData> _receivers { get; } = new();

        public static void InvokeEvent(this Animator animator, string eventName)
        {
            if (animator == null) return;
            
            _receivers
                ?.FirstOrDefault(e => e.Animator.Equals(animator))
                ?.Events
                ?.FirstOrDefault(e => e.Name == eventName)
                ?.Action?.Invoke();
        }

        public static void AddEvent(this Animator animator, string eventName, Action action)
        {
            if (animator == null) return;
            
            if (IsHaveAnimator(animator))
            {
                if (!IsHaveEvent(animator, eventName))
                {
                    _receivers
                        ?.FirstOrDefault(e => e.Animator.Equals(animator))
                        ?.Events
                        ?.Add(new EventData(eventName, action));
                }
                else
                {
                    _receivers
                        ?.FirstOrDefault(e => e.Animator.Equals(animator))
                        ?.Events
                        ?.FirstOrDefault(e => e.Name == eventName)
                        ?.AddEvent(action);
                }
            }
            else
            {
                var receiver = new Animation_ReceiverData(animator);
                receiver.Events.Add(new EventData(eventName, action));
                _receivers.Add(receiver);
            }
        }

        public static void RemoveEvent(this Animator animator, string eventName, Action action)
        {
            if (animator == null) return;
            
            if (IsHaveAnimator(animator))
            {
                if (IsHaveEvent(animator, eventName))
                {
                    _receivers
                        ?.FirstOrDefault(e => e.Animator.Equals(animator))
                        ?.Events
                        ?.FirstOrDefault(e => e.Name == eventName)
                        ?.RemoveEvent(action);
                }
            }
        }

        private static bool IsHaveAnimator(Animator animator)
        {
            return _receivers.Any(e => e.Animator.Equals(animator));
        }

        private static bool IsHaveEvent(Animator animator, string eventName)
        {
            return _receivers
                       ?.FirstOrDefault(e => e.Animator.Equals(animator))
                       ?.Events
                       ?.Any(e => e.Name == eventName) 
                   ?? false;
        }
    }
}