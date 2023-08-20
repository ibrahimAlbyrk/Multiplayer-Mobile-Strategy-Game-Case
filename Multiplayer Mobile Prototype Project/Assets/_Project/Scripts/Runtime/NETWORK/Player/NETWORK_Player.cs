using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon;

namespace Core.Runtime.NETWORK.Player
{
    public class NETWORK_Player : MonoBehaviour
    {
        public int Level { get; private set; }

        [SerializeField] private int _exampleLevel = 1;
        
        private PhotonView _photonView;

        public void SetLevel(int level)
        {
            if(!IsMine()) return;
            
            Level = level;
            
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
            {
                {"MatchLevel", Level}
            });

            PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
        }

        private void Start()
        {
            _photonView = PhotonView.Get(this);

            if (!IsMine()) return;

            SetLevel(_exampleLevel);
        }

        private bool IsMine()
        {
            return _photonView != null && _photonView.IsMine;
        }
    }
}