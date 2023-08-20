using System;
using Photon.Pun;
using Photon.Realtime;

namespace Core.Runtime.NETWORK.Managers
{
    using Singleton;
    
    public class NetworkManager : PunCallbacksSingleton<NetworkManager>
    {
        public static event Action OnClientJoinedLobby;
        
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            OnClientJoinedLobby?.Invoke();
        }
    }
}