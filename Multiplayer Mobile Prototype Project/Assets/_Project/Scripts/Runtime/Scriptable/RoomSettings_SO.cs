using UnityEngine;

namespace Core.Runtime.Scriptable
{
    [CreateAssetMenu(menuName = "Scriptable/Room/Room Settings", fileName = "New Room Setting", order = 0)]
    public class RoomSettings_SO : ScriptableObject
    {
        [field:SerializeField] public int MaxPlayerCount { get; private set; }
    }
}