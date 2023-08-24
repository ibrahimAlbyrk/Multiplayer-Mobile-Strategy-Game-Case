using System;
using System.Collections.Generic;

namespace Core.Runtime.Units.Providers
{
    public class Unit_CommandHandler
    {
        public readonly Unit Unit;

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