using UnityEngine;
using UnityEngine.Events;

namespace Core.Runtime.Animations.Event
{
    [RequireComponent(typeof(Animator))]
    public class EventVisual : MonoBehaviour
    {
        [SerializeField] private Event_VisualData[] _events;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            foreach (var @event in _events)
            {
                _animator.AddEvent(@event.Name,
                    () => @event.Action?.Invoke());
            }
        }
    }

    [System.Serializable]
    public class Event_VisualData
    {
        public string Name;
        public UnityEvent Action;
    }
}