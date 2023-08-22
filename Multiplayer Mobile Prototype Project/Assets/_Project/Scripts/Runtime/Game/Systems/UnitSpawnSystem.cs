using Photon.Pun;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Core.Runtime.Game.Systems
{
    using Units;
    using NETWORK.Instantiate;
    
    public class UnitSpawnSystem : IGameSystemForInit
    {
        private const int _unitCount = 3;
        
        public async void Init()
        {
            await Task.Delay(500);
            
            var room = PhotonNetwork.CurrentRoom;

            if (room == null) return;

            var players = room.Players.Values;

            var playerColors = new List<Color>();

            foreach (var player in players)
            {
                if (!player.CustomProperties.TryGetValue("Color", out var colorProperty))
                {
                    Debug.Log("None");
                    continue;
                }
                
                var floatColor = (float[])colorProperty;
                var color = new Color(floatColor[0], floatColor[1], floatColor[2]);

                playerColors.Add(color);
                
                Debug.Log("Added");
            }

            foreach (var color in playerColors)
            {
                SpawnUnitsWithColor(color, _unitCount);
            }
        }

        private static void SpawnUnitsWithColor(Color color, int count)
        {
            var unitPrefab = Resources.Load<GameObject>("Units/WorkerUnit");

            for (var i = 0; i < count; i++)
            {
                var spawnedUnit = NetworkInstantiater.Instantiate(unitPrefab, Vector3.zero, Quaternion.identity);
                
                var unit = spawnedUnit.GetComponent<Unit>();
            
                unit.SetColor(color);
            }
        }
    }
}