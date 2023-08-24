using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Core.Runtime.Units.Providers
{
    public class Units_CommandProvider : MonoBehaviour
    {
        protected readonly List<Unit_CommandHandler> _CommandHandlers = new();

        public List<Func<bool>> GetCommands(Unit unit)
        {
            return _CommandHandlers.FirstOrDefault(commandHandler => commandHandler.Unit == unit)?.Commands;
        }
        
        public virtual void Init(IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                var commandHandler = new Unit_CommandHandler(unit);
                
                _CommandHandlers.Add(commandHandler);
            }
        }

        public virtual void RunCommands(){}
    }

    public class Unit_CommandHandler
    {
        public Unit Unit;

        public readonly List<Func<bool>> Commands;

        public Unit_CommandHandler(Unit unit)
        {
            Unit = unit;
            Commands = new List<Func<bool>>();
        }

        public void AddCommand(Func<bool> func)
        {
            Commands.Add(func);
        }

        public void RemoveCommand(Func<bool> func)
        {
            Commands.Remove(func);
        }
    }
}