using UnityEngine;

namespace Core.Runtime.Scriptable
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Scriptable/Game/Resource", order = 0)]
    public class Resource_SO : ScriptableObject
    {
        public string Name = "New Resource";
        public string Info = "This is New Resource";
        public int PerAmount = 1;
    }
}