using System;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Collections;
using Core.Runtime.NETWORK;
using ExitGames.Client.Photon;

namespace Core.Runtime.Game.Managers
{
    using Singleton;
    using NETWORK.Scene;
    using NETWORK.Matchmaking;
    
    public class LobbyManager : PunCallbacksSingleton<LobbyManager>
    {
        public static event Action OnRoomStarted;
        
        private Coroutine _startGameCoroutine;
        
        private readonly RaiseEventOptions eventOptions = new() { Receivers = ReceiverGroup.All };
        
        #region Pun Callbacks

        public override void OnPlayerEnteredRoom(Player newPlayer) => CheckStartGame();

        public override void OnPlayerLeftRoom(Player otherPlayer) => CheckStartGame();

        #endregion

        #region Base methods

        protected override void Awake()
        {
            base.Awake();

            NETWORK_RoomManager.OnClientJoinedRoom += CheckStartGame;
            NETWORK_RoomManager.OnClientExitedRoom += CheckStartGame;
        }

        private void OnDestroy()
        {
            NETWORK_RoomManager.OnClientJoinedRoom -= CheckStartGame;
            NETWORK_RoomManager.OnClientExitedRoom -= CheckStartGame;
        }

        #endregion
        
        private void CheckStartGame()
        {
            if (!PhotonNetwork.IsMasterClient) return;
            
            if (PhotonNetwork.CurrentRoom == null) return;
            
            var isRoomFull = PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers;
            
            //Hide the room from matchmaking if the room is full, show it in matchmaking if there is room in the room
            NETWORK_RoomManager.Instance.SetVisibleCurrentRoom(!isRoomFull);

            //start the game if the room is full
            if (isRoomFull)
            {
                if (_startGameCoroutine == null)
                {
                    PhotonNetwork.RaiseEvent(NETWORK_EventCode.SEND_ROOM_STATE_EVENT_CODE,
                        true,
                        eventOptions,
                        SendOptions.SendReliable);
                    
                    _startGameCoroutine = StartCoroutine(StartGame_Cor());   
                }
                return;
            }
            
            //stop game launch if room is not full and coroutine is full
            if (_startGameCoroutine == null) return;
            
            StopCoroutine(_startGameCoroutine);
            _startGameCoroutine = null;
            
            PhotonNetwork.RaiseEvent(NETWORK_EventCode.SEND_ROOM_STATE_EVENT_CODE,
                false,
                eventOptions,
                SendOptions.SendReliable);
        }

        private IEnumerator StartGame_Cor()
        {
            //A 3 second delay is given in case the client wants to leave the room.
            yield return new WaitForSeconds(3f);
            
            NETWORK_SceneLoader.LoadScene("Game_Scene");
            
            OnRoomStarted?.Invoke();
        }
    }
}