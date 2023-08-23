using Photon.Pun;
using UnityEngine;
using System.Linq;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;

namespace Core.Runtime.Game.Systems
{
    using NETWORK;
    using Utilities;

    public class CharacterDeterminantSystem : IGameSystemForInitAndReset, IOnEventCallback
    {
        private readonly List<Color> _playerColors = new();

        private const float _colorDistanceThreshold = .2f;
        private const int _maxAttemptsColor = 10;

        public void Init()
        {
            PhotonNetwork.AddCallbackTarget(this);
            
            if (!PhotonNetwork.IsMasterClient) return;
            
            var currentRoom = PhotonNetwork.CurrentRoom;

            if (currentRoom == null) return;

            var players = currentRoom.Players.Values;

            foreach (var player in players)
            {
                var color = GetUniqueColor();

                _playerColors.Add(color);

                //Convert Color to float array
                var colorValues = new[] { color.r, color.g, color.b };
                
                //Add the color values to the custom properties of the player.
                var property = new Hashtable
                {
                    { "ActorNumber", player.ActorNumber },
                    { "Color", colorValues }
                };

                PhotonNetwork.RaiseEvent(NETWORK_EventCode.CHANGE_PLAYER_COLOR_EVENT_CODE, property,
                    new RaiseEventOptions { Receivers = ReceiverGroup.All },
                    SendOptions.SendReliable);
            }
        }

        public void Reset()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
        
        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == NETWORK_EventCode.CHANGE_PLAYER_COLOR_EVENT_CODE)
            {
                if (photonEvent.CustomData is not Hashtable data) return;
                ChangePlayerColor(data);
            }
        }

        private static void ChangePlayerColor(Hashtable data)
        {
            if (data["ActorNumber"] is not int actorNumber) return;

            if (!PhotonNetwork.CurrentRoom.Players.ContainsKey(actorNumber)) return;

            var player = PhotonNetwork.CurrentRoom.Players[actorNumber];

            var colorData = data["Color"];

            if (colorData == null) return;

            //This client takes new color values from MasterClient and sets them to their CustomProperties.
            var colorProperty = new Hashtable { { "Color", colorData } };

            player.SetCustomProperties(colorProperty);
        }

        #region Utilities

        private Color GetUniqueColor()
        {
            for (var i = 0; i < _maxAttemptsColor; i++)
            {
                var color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.7f, 1f);
                if (!IsColorUnique(color)) continue;

                return color;
            }

            // Could not generate a unique color
            return default;
        }

        private bool IsColorUnique(Color color)
        {
            return _playerColors
                .All(existingColor => !(ColorUtilities.ColorDistance(color, existingColor) < _colorDistanceThreshold));
        }

        #endregion
    }
}