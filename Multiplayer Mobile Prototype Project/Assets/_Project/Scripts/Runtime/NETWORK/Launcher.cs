using Photon.Pun;
using UnityEngine;

namespace Core.Runtime.NETWORK
{
    public class Launcher : MonoBehaviour
    {
        #region Private Vars

        private const string _gameVersion = "V0.1";

        #endregion

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
    }
}