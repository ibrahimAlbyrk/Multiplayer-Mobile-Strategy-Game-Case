using System;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Core.Runtime.Popup
{
    public class PopupHandler
    {
        private readonly ObjectPool<UI_Popup> Pool;

        private readonly Transform _content;

        private readonly GameObject _prefab;

        private readonly Dictionary<string, UI_Popup> _openedPopups;
        
        public PopupHandler(Transform content, GameObject prefab)
        {
            Pool = new ObjectPool<UI_Popup>(Create);

            _openedPopups = new Dictionary<string, UI_Popup>();

            _content = content;

            _prefab = prefab;
        }

        public void Open(string popupName, Action OnClose)
        {
            if (_openedPopups.ContainsKey(popupName)) return;
            
            var popup = Pool.Get();
            
            popup.Open(OnClose);
            
            _openedPopups.Add(popupName, popup);
        }

        public void Close(string popupName)
        {
            if (!_openedPopups.ContainsKey(popupName)) return;

            var openedPopup = _openedPopups[popupName];
            
            openedPopup.Close();
            
            Pool.Release(openedPopup);
        }

        #region Pool methods

        private UI_Popup Create()
        {
            if (_prefab == null) return null;
            
            var popupInstant = Object.Instantiate(_prefab, _content);

            return popupInstant.TryGetComponent(out UI_Popup popup)
                ? popup
                : null;
        }

        #endregion
    }
}