using Core.Runtime.Commands;
using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    [System.Serializable]
    public abstract class Unit_Command<T> : Command<Transform, T> where T : class
    {
        protected Unit _unit;

        public virtual void Init(Unit unit)
        {
            _unit = unit;
        }
    }
    
    public abstract class Unit_Transform_Command : Unit_Command<Transform> { }
}