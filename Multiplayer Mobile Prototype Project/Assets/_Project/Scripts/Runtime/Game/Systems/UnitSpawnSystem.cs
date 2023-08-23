using System.Linq;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Threading.Tasks;
using ExitGames.Client.Photon;

namespace Core.Runtime.Game.Systems
{
    using NETWORK;
    using Units.Managers;

    public class UnitSpawnSystem : IGameSystemForInitAndReset, IOnEventCallback
    {
        private const int _unitCount = 3;

        public async void Init()
        {
            PhotonNetwork.AddCallbackTarget(this);

            if (!PhotonNetwork.IsMasterClient) return;

            //TODO: Will be changed later
            await Task.Delay(500);

            var room = PhotonNetwork.CurrentRoom;

            if (room == null) return;

            var players = room.Players.Values;

            foreach (var spawnUnitHashtable in players.Select(player => new Hashtable {{ "ActorNumber", player.ActorNumber },}))
            {
                PhotonNetwork.RaiseEvent(
                    NETWORK_EventCode.SPAWN_UNIT_EVENT_CODE,
                    spawnUnitHashtable,
                    new RaiseEventOptions { Receivers = ReceiverGroup.All },
                    SendOptions.SendReliable);
            }
        }

        public void Reset()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        private static void SpawnUnitsWithColor(float[] colorArray, int count)
        {
            var color = new Color(colorArray[0], colorArray[1], colorArray[2]);
            
            UnitManager.Instance?.SpawnUnitsWithColor(color, count);
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.CustomData is not Hashtable data) return;
            if (data["ActorNumber"] is int actorNumber)
            {
                if (actorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    return; //This event is not for this player
                }
            }

            if (photonEvent.Code == NETWORK_EventCode.SPAWN_UNIT_EVENT_CODE)
            {
                if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Color")) return;

                var colorArray = (float[])PhotonNetwork.LocalPlayer.CustomProperties["Color"];

                SpawnUnitsWithColor(colorArray, _unitCount);
            }
        }
    }
}