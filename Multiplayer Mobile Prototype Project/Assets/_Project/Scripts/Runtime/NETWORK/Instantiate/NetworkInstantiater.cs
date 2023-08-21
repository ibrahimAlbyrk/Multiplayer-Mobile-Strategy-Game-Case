using Photon.Pun;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Runtime.NETWORK.Instantiate
{
    public class NetworkInstantiater
    {
        private static readonly List<NetworkPrefab> _networkPrefabs = new();
        
        public static GameObject Instantiate(GameObject obj, Vector3 position = default, Quaternion rotation = default, Transform parent = default)
        {
            foreach (var instantObj in from networkPrefab in _networkPrefabs
                     where networkPrefab.Prefab == obj
                     where networkPrefab.Path != string.Empty
                     select PhotonNetwork.Instantiate(networkPrefab.Path, position, rotation))
            {
                instantObj.transform.SetParent(parent);
                return instantObj;
            }

            return null;
        }

        #if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void LoadNetworkPrefabs()
        {
            _networkPrefabs.Clear();
            
            var prefabs = Resources.LoadAll<GameObject>("");

            foreach (var prefab in prefabs)
            {
                if(!prefab.TryGetComponent(out PhotonView _)) continue;

                var path = UnityEditor.AssetDatabase.GetAssetPath(prefab);

                var networkPrefab = new NetworkPrefab(prefab, path);
                
                _networkPrefabs.Add(networkPrefab);
            }
        }
        #endif
    }
}