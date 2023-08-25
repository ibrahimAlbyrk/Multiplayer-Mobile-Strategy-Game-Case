using UnityEngine;
using System.Collections.Generic;

namespace Core.Runtime.NETWORK.Instantiate
{
    [CreateAssetMenu(fileName = "New Network Instant data", menuName = "Scriptable/Network/Instant Data", order = 0)]
    public class NetworkInstantData : ScriptableObject
    {
        public List<NetworkPrefab> NetworkPrefabs = new();
    }
}