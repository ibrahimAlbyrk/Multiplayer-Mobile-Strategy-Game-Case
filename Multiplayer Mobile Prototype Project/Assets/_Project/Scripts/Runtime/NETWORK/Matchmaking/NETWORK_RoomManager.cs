using System;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace Core.Runtime.NETWORK.Matchmaking
{
    using Singleton;
    using Scriptable;

    public class NETWORK_RoomManager : PunCallbacksSingleton<NETWORK_RoomManager>
    {
        public static event Action OnClientCreatedRoom;
        public static event Action OnClientJoinedRoom;
        public static event Action OnClientExitedRoom;

        [SerializeField] private RoomSettings_SO _roomSettings;

        public void StartMatchmaking()
        {
            JoinRandomWithMatching(_roomSettings.MaxPlayerCount);
        }
        
        public void SetVisibleCurrentRoom(bool visible)
        {
            if (!HasConnected() || !PhotonNetwork.IsMasterClient) return;

            if (PhotonNetwork.CurrentRoom == null) return;

            PhotonNetwork.CurrentRoom.IsOpen = visible;
            PhotonNetwork.CurrentRoom.IsVisible = visible;
        }
        
        public void CreateRoomWithMatching()
        {
            if (!HasConnected()) return;
            
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
            {
                {"MatchLevel", 1}
            });

            PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);

            var playerMatchLevel = (int)PhotonNetwork.LocalPlayer.CustomProperties["MatchLevel"];

            //The level information in custom room properties is given as an example.
            var roomOptions = new RoomOptions
            {
                IsVisible = true,
                MaxPlayers = _roomSettings.MaxPlayerCount,
                CustomRoomProperties = new Hashtable { { "MatchLevel", playerMatchLevel } },
                CustomRoomPropertiesForLobby = new[] { "MatchLevel" }
            };

            //A null value is entered to make the name unique.
            PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
        }

        public void CreateRoom()
        {
            if (!HasConnected()) return;

            //The level information in custom room properties is given as an example.
            var roomOptions = new RoomOptions
            {
                IsVisible = true,
                MaxPlayers = _roomSettings.MaxPlayerCount,
            };

            //A null value is entered to make the name unique.
            PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
        }

        public void JoinRandomWithMatching(int expectedMaxPlayers = 0)
        {
            if (!HasConnected()) return;
            
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
            {
                {"MatchLevel", 1}
            });

            PhotonNetwork.SetPlayerCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);

            var playerMatchLevel = (int)PhotonNetwork.LocalPlayer.CustomProperties["MatchLevel"];

            var roomOptions = new RoomOptions
            {
                CustomRoomProperties = new Hashtable { { "MatchLevel", playerMatchLevel } },
                CustomRoomPropertiesForLobby = new[] { "MatchLevel" }
            };

            PhotonNetwork.JoinRandomRoom(roomOptions.CustomRoomProperties, (byte)expectedMaxPlayers);
        }

        public void JoinRoom()
        {
            if (!HasConnected()) return;

            PhotonNetwork.JoinRandomRoom();
        }

        public void ExitRoom()
        {
            if (!HasConnected()) return;

            PhotonNetwork.LeaveRoom();
        }

        #region Failed Callbacks

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            CreateRoomWithMatching();
        }

        #endregion

        #region Successfull Callbacks

        public override void OnCreatedRoom()
        {
            Debug.Log("Room Created");

            OnClientCreatedRoom?.Invoke();
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            print("entered room, room manager");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined To Room");

            OnClientJoinedRoom?.Invoke();
        }

        public override void OnLeftRoom()
        {
            Debug.Log("Left To Room");

            OnClientExitedRoom?.Invoke();
        }

        #endregion

        #region Utilities

        private bool HasConnected()
        {
            return PhotonNetwork.IsConnected;
        }

        #endregion
    }
}