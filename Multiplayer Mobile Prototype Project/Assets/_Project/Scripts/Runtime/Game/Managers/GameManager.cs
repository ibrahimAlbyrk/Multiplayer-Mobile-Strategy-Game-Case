using Core.Runtime.NETWORK;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace Core.Runtime.Game.Managers
{
    using Systems;
    using Singleton;   
    
    public class GameManager : PunCallbacksSingleton<GameManager>, IOnEventCallback
    {
        [SerializeField] private Image myColorImage;
        
        private GameSystemsHandler _gameSystemsHandler;

        public void SetMyColor(float[] colorArray) => myColorImage.color = new Color(colorArray[0], colorArray[1], colorArray[2]);

        protected override void Awake()
        {
            base.Awake();

            _gameSystemsHandler = new GameSystemsHandler();
            
            _gameSystemsHandler.AddGameSystemForInit(new CollectibleSpawnSystem());
            _gameSystemsHandler.AddGameSystemForInitAndReset(new CharacterDeterminantSystem());
            _gameSystemsHandler.AddGameSystemForInitAndReset(new UnitSpawnSystem());
        }

        private void Update()
        {
            if (!Init()) return;
            
            _gameSystemsHandler.Update();
        }

        private void FixedUpdate()
        {
            if (!Init()) return;
            
            _gameSystemsHandler.FixedUpdate();
        }
        
        #region Utilities

        private static bool Init()
        {
            return PhotonNetwork.IsMasterClient;
        }

        #endregion

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code != NETWORK_EventCode.DESTROY_OBJECT_EVENT_CODE) return;
            
            var viewID = (int)photonEvent.CustomData;

            var obj = FindPhotonViewByID(viewID)?.gameObject;

            if (obj == null) return;
                
            PhotonNetwork.Destroy(obj);
        }
        
        private static PhotonView FindPhotonViewByID(int id)
        {
            foreach (var view in PhotonNetwork.PhotonViewCollection)
            {
                if (view.ViewID == id)
                {
                    return view;
                }
            }

            return null; // Not found
        }
    }
}