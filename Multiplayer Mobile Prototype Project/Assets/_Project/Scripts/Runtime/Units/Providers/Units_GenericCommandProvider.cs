using System.Linq;

namespace Core.Runtime.Units.Providers
{
    public class Units_GenericCommandProvider : Units_CommandProvider
    {
        /// <summary>
        /// Completes the executed commands in order and removes
        /// them from the list.
        /// </summary>
        public override void RunCommands()
        {
            foreach (var Commands in m_CommandHandlers.Select(commandHandler => commandHandler.Commands))
            {
                if (Commands.Count < 1) continue;

                var task = Commands[0];

                var isCompleted = task.Invoke();

                if (!isCompleted) continue;

                Commands.RemoveAt(0);
            }
        }
    }
}