using UnityEngine;

namespace Core.Runtime.NETWORK.Managers
{
    using Singleton;
    
    public class NetworkManager : PunCallbacksSingleton<NetworkManager>
    {
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connect to Server");
        }
    }
}