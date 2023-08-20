using Photon.Pun;
using UnityEngine;

namespace Core.Runtime.NETWORK
{
    public class Launcher : MonoBehaviour
    {
        private const string _gameVersion = "V0.1";

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