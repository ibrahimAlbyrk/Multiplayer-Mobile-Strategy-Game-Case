using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Core.Runtime.Units.Providers
{
    public class Units_CommandProvider : MonoBehaviour
    {
        protected readonly List<Unit_CommandHandler> m_CommandHandlers = new();

        public Unit_CommandHandler GetCommandHandler(Unit unit)
        {
            return m_CommandHandlers.FirstOrDefault(commandHandler => commandHandler.Unit == unit);
        }
        
        public List<Func<bool>> GetCommands(Unit unit)
        {
            return GetCommandHandler(unit)?.Commands;
        }
        
        public virtual void Init(IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                var commandHandler = new Unit_CommandHandler(unit);
                
                m_CommandHandlers.Add(commandHandler);
            }
        }

        public virtual void RunCommands(){}
    }
}