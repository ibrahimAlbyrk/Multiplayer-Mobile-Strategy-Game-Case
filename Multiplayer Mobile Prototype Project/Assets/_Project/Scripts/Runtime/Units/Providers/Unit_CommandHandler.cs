using System;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Runtime.Units.Providers
{
    public class Unit_CommandHandler : MonoBehaviour
    {
        public Unit Unit;

        public List<Func<bool>> Commands;

        public void Init(Unit unit)
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