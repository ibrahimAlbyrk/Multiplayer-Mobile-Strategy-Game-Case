using System;

namespace Core.Runtime.Animations.Event
{
    [Serializable]
    public class EventData
    {
        public string Name;
        public Action Action;
        
        public EventData(string name, Action action)
        {
            Name = name;
            Action = action;
        }
        
        public void AddEvent(Action action)
        {
            Action += action;
        }
        
        public void RemoveEvent(Action action)
        {
            Action -= action;
        }
    }
}