using System;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Runtime.Popup
{
    using Singleton;
    
    public class PopupManager : MonoBehaviorSingleton<PopupManager>
    {
        [SerializeField] private Transform _content;

        private readonly Dictionary<PopupType, PopupHandler> _popupHandlers = new();

        private readonly Dictionary<PopupType, GameObject> _popupPrefabs = new();

        public void Open(PopupType type, string popupName, Action OnClose = null)
        {
            var handler = GetHandler(type);

            if (handler == null)
            {
                GameObject popupPrefab = null;

                if (_popupPrefabs.TryGetValue(type, out var prefab))
                    popupPrefab = prefab;
                
                handler = new PopupHandler(_content, popupPrefab);
                _popupHandlers.Add(type, handler);
            }
            
            handler.Open(popupName, OnClose);
        }

        public void Close(PopupType type, string popupName)
        {
            var handler = GetHandler(type);

            handler?.Close(popupName);
        }

        protected override void Awake()
        {
            base.Awake();

            LoadPopupPrefabs();
        }
        
        #region Utilities

        private PopupHandler GetHandler(PopupType type)
        {
            return _popupHandlers.TryGetValue(type, out var handler)
                ? handler
                : null;
        }

        private void LoadPopupPrefabs()
        {
            foreach (PopupType type in System.Enum.GetValues(typeof(PopupType)))
            {
                var prefab = Resources.Load<GameObject>($"UI/Popups/UI_{type}_Popup");
                _popupPrefabs.Add(type, prefab);
            }
        }

        #endregion
    }
}