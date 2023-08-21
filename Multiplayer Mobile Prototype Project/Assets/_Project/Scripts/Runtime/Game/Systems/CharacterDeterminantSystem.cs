using Photon.Pun;
using UnityEngine;
using System.Linq;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;

namespace Core.Runtime.Game.Systems
{
    using Utilities;

    public class CharacterDeterminantSystem : IGameSystemForInitAndReset, IOnEventCallback
    {
        private readonly List<Color> _playerColors = new();

        private const float _colorDistanceThreshold = .2f;
        private const int _maxAttemptsColor = 10;

        private const byte CHANGE_COLOR_EVENT_CODE = 1;

        public void Init()
        {
            var currentRoom = PhotonNetwork.CurrentRoom;

            PhotonNetwork.AddCallbackTarget(this);

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
                    { "PlayerId", player.UserId },
                    { "Color", colorValues }
                };

                var raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

                PhotonNetwork.RaiseEvent(CHANGE_COLOR_EVENT_CODE, property,
                    raiseEventOptions,
                    SendOptions.SendReliable);
            }
        }

        public void Reset()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
        
        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == CHANGE_COLOR_EVENT_CODE)
            {
                if (photonEvent.CustomData is not Hashtable data) return;
                ChangeLocalPlayerColor(data);
            }
        }

        private static void ChangeLocalPlayerColor(Hashtable data)
        {
            if (data["PlayerId"] is string playerId)
            {
                if (playerId != PhotonNetwork.LocalPlayer.UserId)
                {
                    return; //This event is not for us
                }
            }

            var colorData = data["Color"];

            if (colorData == null) return;

            //This client takes new color values from MasterClient and sets them to their CustomProperties.
            var colorProperty = new Hashtable { { "Color", colorData } };

            PhotonNetwork.LocalPlayer.SetCustomProperties(colorProperty);

            var debugColor = (float[])colorProperty["Color"];
            
            Debug.Log($"Setted color for player: ({debugColor[0]}, {debugColor[1]}, {debugColor[2]})");
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