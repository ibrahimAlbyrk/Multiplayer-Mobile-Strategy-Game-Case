using Photon.Pun;
using UnityEngine;

namespace Core.Runtime.Singleton
{
    using Utilities;
    
    public class PunCallbacksSingleton<T> : MonoBehaviourPunCallbacks where T : class
    {
        public static T Instance;

        [Header("Singleton Settings")]
        [SerializeField] private bool _isDontDestroy;
        
        protected virtual void Awake()
        {
            SingletonUtilities.Init(ref Instance, gameObject, _isDontDestroy);
        }
    }
}