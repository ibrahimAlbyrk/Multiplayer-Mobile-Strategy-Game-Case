using Core.Runtime.NETWORK;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Runtime.Runtime.UI
{
    using Game.Managers;
    using NETWORK.Managers;
    using NETWORK.Matchmaking;
    
    public class UI_Lobby : MonoBehaviour, IOnEventCallback
    {
        [SerializeField] private Button _matchmakingButton;
        
        [Header("Matchmaking Settings")]
        [SerializeField] private GameObject _matchmakingPanel;
        [SerializeField] private Button _exitMatchmakingButton;
        [SerializeField] private TMP_Text _matchmakingPanelText;

        private void Awake()
        {
            _matchmakingButton.interactable = false;
            _matchmakingButton.onClick.AddListener(MatchmakingHandler);

            NetworkManager.OnClientJoinedLobby += OnJoinedLobby;
            
            _exitMatchmakingButton.onClick.AddListener(ExitMatchmaking);

            PhotonNetwork.AddCallbackTarget(this);
        }
        
        private void OnDestroy()
        {
            NetworkManager.OnClientJoinedLobby -= OnJoinedLobby;
            
            PhotonNetwork.RemoveCallbackTarget(this);
        }


        private void StartingGame(bool isStarting)
        {
            _matchmakingPanelText.text = isStarting
                ? "Match Starting..."
                : "Searching Room...";
        }
        private void ExitMatchmaking()
        {
            NETWORK_RoomManager.Instance.ExitRoom();
            _matchmakingPanel.SetActive(false);
        }

        private void MatchmakingHandler()
        {
            NETWORK_RoomManager.Instance.StartMatchmaking();
            
            _matchmakingPanel.SetActive(true);
        }

        private void OnJoinedLobby()
        {
            _matchmakingButton.interactable = true;
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == NETWORK_EventCode.SEND_ROOM_STATE_EVENT_CODE)
            {
                var isStarting = (bool)photonEvent.CustomData;

                StartingGame(isStarting);
            }
        }
    }
}