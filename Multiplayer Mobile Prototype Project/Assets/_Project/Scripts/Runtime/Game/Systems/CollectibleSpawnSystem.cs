using UnityEngine;

namespace Core.Runtime.Game.Systems
{
    using NETWORK.Instantiate;
    
    public class CollectibleSpawnSystem : IGameSystemForInit
    {
        private const int _count = 7;
        
        public void Init()
        {
            var collectibles = Resources.LoadAll<GameObject>("Collectible");

            for (var i = 0; i < _count; i++)
            {
                var collectible = collectibles[Random.Range(0, collectibles.Length)];
                
                var x = Random.Range(-17, 25);
                var z = Random.Range(-18, 22);

                NetworkInstantiater.Instantiate(collectible, new Vector3(x, 0, z),
                    Quaternion.Euler(0, Random.Range(0, 360), 0));
            }
        }
    }
}