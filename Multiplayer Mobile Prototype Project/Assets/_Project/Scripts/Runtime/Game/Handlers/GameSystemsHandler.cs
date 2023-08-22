using System.Collections.Generic;

namespace Core.Runtime.Game.Managers
{
    using Systems;
    
    public class GameSystemsHandler
    {
        private readonly List<IGameSystemForInit> _gameSystemsForInit = new();
        private readonly List<IGameSystemForInitAndReset> _gameSystemsForInitAndReset = new();
        private readonly List<IGameSystemForRepeat> _gameSystemsForRepeat = new();
        private readonly List<IGameSystemForCompletely> _gameSystemsForCompletely = new();

        #region Update Methods

        public void Update()
        {
            _gameSystemsForRepeat.ForEach(system => system.Update());
            _gameSystemsForCompletely.ForEach(system => system.Update());
        }

        public void FixedUpdate()
        {
            _gameSystemsForRepeat.ForEach(system => system.Update());
            _gameSystemsForCompletely.ForEach(system => system.Update());
        }

        #endregion
        
        #region Add methods

        public void AddGameSystemForInit(IGameSystemForInit system)
        {
            if (_gameSystemsForInit.Contains(system)) return;
            
            _gameSystemsForInit.Add(system);
            
            system.Init();
        }
        
        public void AddGameSystemForInitAndReset(IGameSystemForInitAndReset system)
        {
            if (_gameSystemsForInitAndReset.Contains(system)) return;
            
            _gameSystemsForInitAndReset.Add(system);
            
            system.Init();
        }
        
        public void AddGameSystemForRepeat(IGameSystemForRepeat system)
        {
            if (_gameSystemsForRepeat.Contains(system)) return;
            
            _gameSystemsForRepeat.Add(system);
        }
        
        public void AddGameSystemForCompletely(IGameSystemForCompletely system)
        {
            if (_gameSystemsForCompletely.Contains(system)) return;
            
            _gameSystemsForCompletely.Add(system);
            
            system.Init();
        }

        #endregion
        
        #region Remove Methods

        public void RemoveGameSystemForInit(IGameSystemForInit system)
        {
            _gameSystemsForInit.Remove(system);
        }
        
        public void RemoveGameSystemForInitAndReset(IGameSystemForInitAndReset system)
        {
            if(!_gameSystemsForInitAndReset.Remove(system)) return;
            
            system.Reset();
        }
        
        public void RemoveGameSystemForRepeat(IGameSystemForRepeat system)
        {
            _gameSystemsForRepeat.Remove(system);
        }
        
        public void RemoveGameSystemForCompletely(IGameSystemForCompletely system)
        {
            if(!_gameSystemsForCompletely.Remove(system)) return;
            
            system.Reset();
        }

        #endregion
    }
}