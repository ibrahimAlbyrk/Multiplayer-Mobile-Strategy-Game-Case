using Photon.Pun;

namespace Core.Runtime.Game.Managers
{
    using Singleton;
    using Systems;
    
    public class GameManager : PunCallbacksSingleton<GameManager>
    {
        private GameSystemsHandler _gameSystemsHandler;

        protected override void Awake()
        {
            base.Awake();

            _gameSystemsHandler = new GameSystemsHandler();
            
            _gameSystemsHandler.AddGameSystemsForInitAndReset(new CharacterDeterminantSystem());
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
    }
}