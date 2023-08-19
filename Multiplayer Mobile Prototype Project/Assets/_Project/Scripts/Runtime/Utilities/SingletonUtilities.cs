using UnityEngine;

namespace Core.Runtime.Utilities
{
    public static class SingletonUtilities
    {
        public static void Init<T>(ref T instance, GameObject obj, bool isDontDestroy) where T : class
        {
            if (instance != null)
            {
                Object.Destroy(obj);
                return;
            }

            instance = obj.GetComponent<T>();

            if (!isDontDestroy) return;
            
            if(obj.transform.parent != null)
                obj.transform.SetParent(null);
            
            Object.DontDestroyOnLoad(obj);
        }
    }
}