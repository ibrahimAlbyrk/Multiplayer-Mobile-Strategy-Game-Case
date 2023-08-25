using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Runtime.Popup
{
    public class UI_Popup : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private Transform _content;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Buttons")]
        [SerializeField] private Button _closeButton;

        [Header("Settings")]
        [SerializeField] private float _transitionTime = 1f;

        [Header("Animations")]
        [SerializeField] private Ease OpenAnimation = Ease.OutBounce;
        [SerializeField] private Ease CloseAnimation = Ease.OutBounce;
        
        public void Open(Action OnClose)
        {
            _content.gameObject.SetActive(true);
            
            _canvasGroup.DOFade(1, _transitionTime);
            _content.DOScale(Vector3.one, _transitionTime).SetEase(OpenAnimation);
            
            _closeButton.onClick.AddListener(() => OnClose?.Invoke());
        }

        public void Close()
        {
            _canvasGroup.DOFade(0, _transitionTime);
            _content.DOScale(Vector3.zero, _transitionTime).SetEase(CloseAnimation)
                .OnComplete(ResetHandler);
        }

        public void ResetHandler()
        {
            _content.gameObject.SetActive(false);
            
            _closeButton.onClick.RemoveAllListeners();
            _closeButton.onClick.AddListener(Close);
        }

        #region Base Methods

        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
        }

        #endregion
    }
}