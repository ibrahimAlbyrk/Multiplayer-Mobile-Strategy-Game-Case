using System;
using System.Linq;
using System.Collections.Generic;

namespace Core.Runtime.Units.Providers
{
    public class Units_CommandProvider
    {
        protected readonly List<Unit_CommandHandler> m_CommandHandlers = new();

        public void ResetCommands(Unit unit)
        {
            var commandHandler = GetCommandHandler(unit);

            commandHandler.Commands = new List<Func<bool>>();
        }
        
        public void AddCommandHandler(Unit unit)
        {
            var commandHandler = unit.gameObject.AddComponent<Unit_CommandHandler>();
            commandHandler.Init(unit);
            
            m_CommandHandlers.Add(commandHandler);
        }

        public void RemoveCommandHandler(Unit unit)
        {
            var commandHandler = m_CommandHandlers.FirstOrDefault(handler => handler.Unit == unit);

            m_CommandHandlers.Remove(commandHandler);
        }
        
        public Unit_CommandHandler GetCommandHandler(Unit unit)
        {
            return m_CommandHandlers.FirstOrDefault(commandHandler => commandHandler.Unit == unit);
        }
        
        public List<Func<bool>> GetCommands(Unit unit)
        {
            return GetCommandHandler(unit)?.Commands;
        }

        public virtual void RunCommands(){}
    }
}