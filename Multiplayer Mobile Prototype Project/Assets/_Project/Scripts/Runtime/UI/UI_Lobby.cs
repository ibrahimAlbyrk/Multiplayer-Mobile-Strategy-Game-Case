using UnityEngine;
using UnityEngine.UI;

namespace Core.Runtime.Runtime.UI
{
    using NETWORK.Managers;
    using NETWORK.Matchmaking;
    
    public class UI_Lobby : MonoBehaviour
    {
        [SerializeField] private Button _matchmakingButton;

        private void Awake()
        {
            _matchmakingButton.interactable = false;
            _matchmakingButton.onClick.AddListener(MatchmakingHandler);

            NetworkManager.OnClientJoinedLobby += OnJoinedLobby;
        }

        private void OnDestroy()
        {
            NetworkManager.OnClientJoinedLobby -= OnJoinedLobby;
        }

        private void MatchmakingHandler()
        {
            NETWORK_RoomManager.Instance.StartMatchmaking();
        }

        private void OnJoinedLobby()
        {
            _matchmakingButton.interactable = true;
        }
    }
}