using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    public class Unit_MoveCommand : Unit_Transform_Command
    {
        public override bool Execute(Transform obj1, Transform obj2)
        {
            return false;
        }
    }
}