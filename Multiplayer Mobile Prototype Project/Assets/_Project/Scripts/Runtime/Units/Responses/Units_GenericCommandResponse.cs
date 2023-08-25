using System;

namespace Core.Runtime.Units.Responses
{
    using Types;

    public class Units_GenericCommandResponse : Units_CommandResponse
    {
        /// <summary>
        /// The command of the specified command type with the specified
        /// parameter is added to the tasks. If the tasks are
        /// not working, the tasks will be initiated.
        /// </summary>
        /// /// <param name="commandObject"></param>
        /// <param name="commandTypes"></param>
        public override void SendCommand(Unit unit, object commandObject, params Unit_CommandType[] commandTypes)
        {
            foreach (var commandType in commandTypes)
            {
                //TODO: will be changed for later

                var command = Type.GetType($"Core.Runtime.Units.Commands.Unit_{commandType}Command")!;

                if (command == null)
                    throw new ArgumentNullException($"Generic Command Response", "Command Type is null");

                var command_obj = Activator.CreateInstance(command);

                var init = command.GetMethod("Init");

                init?.Invoke(command_obj, new object[] { unit });

                var commandHandler = m_provider.GetCommandHandler(unit);
                
                commandHandler.AddCommand(Execute);

                continue;

                bool Execute()
                {
                    var isCompleted = (bool)command.GetMethod("Execute")
                        ?.Invoke(command_obj, new[] { unit.transform, commandObject })!;

                    return isCompleted;
                }
            }
        }

        /// <summary>
        /// The command of the specified command type with the specified
        /// parameter is added to the tasks. If the tasks are
        /// not working, the tasks will be initiated.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandObject"></param>
        public override void SendCommand(Unit unit, Unit_CommandType commandType, object commandObject)
        {
            SendCommand(unit, commandObject, commandType);
        }
    }
}