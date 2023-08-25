using Photon.Pun;
using System.Linq;
using UnityEngine;

namespace Core.Runtime.NETWORK.Instantiate
{
    public class NetworkInstantiater
    {
        private static NetworkInstantData _networkInstantData;

        public static NetworkInstantData NetworkInstantData
        {
            get
            {
                if(_networkInstantData == null)
                    _networkInstantData = Resources.Load<NetworkInstantData>("Data/NetworkInstantData");
                return _networkInstantData;
            }
        }
        
        public static GameObject Instantiate(GameObject obj, Vector3 position = default, Quaternion rotation = default, Transform parent = default)
        {
            foreach (var instantObj in from networkPrefab in NetworkInstantData.NetworkPrefabs
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
            NetworkInstantData.NetworkPrefabs?.Clear();
            
            var prefabs = Resources.LoadAll<GameObject>("");

            foreach (var prefab in prefabs)
            {
                if(!prefab.TryGetComponent(out PhotonView _)) continue;

                var path = UnityEditor.AssetDatabase.GetAssetPath(prefab);

                var networkPrefab = new NetworkPrefab(prefab, path);
                
                NetworkInstantData.NetworkPrefabs?.Add(networkPrefab);
            }
        }
        #endif
    }
}