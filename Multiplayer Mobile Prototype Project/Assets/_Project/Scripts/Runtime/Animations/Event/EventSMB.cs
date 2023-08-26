using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Runtime.Animations.Event
{
    public class EventSMB : StateMachineBehaviour
    {
        [Header("Events")]
        [SerializeField] private List<Animation_EventData> _events;

        private int _currentFrame;
        private int _frameCounter;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_events.Count < 1) return;

            var _currentClip = animator.GetCurrentAnimatorClipInfo(layerIndex)[0];

            var weight = stateInfo.normalizedTime % 1;

            _currentFrame = (int)(weight * (_currentClip.clip.length * _currentClip.clip.frameRate));

            if (_frameCounter == _currentFrame) return;

            _frameCounter = _currentFrame;

            foreach (var _event in _events.Where(_event => _currentFrame == _event.Frame))
            {
                animator.InvokeEvent(_event.Name);
            }
        }

        public void AddEvent()
        {
            _events ??= new List<Animation_EventData>();

            _events.Add(_events.Count < 1
                ? new Animation_EventData { Name = "New Event" }
                : new Animation_EventData { Name = _events[^1].Name, Frame = _events[^1].Frame });
        }

        public void RemoveEvent()
        {
            if (_events.Count < 1) return;

            _events.RemoveAt(_events.Count - 1);
        }
    }
}