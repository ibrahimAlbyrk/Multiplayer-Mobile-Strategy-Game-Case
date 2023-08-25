using UnityEngine;

namespace Core.Runtime.Collectible.Managers
{
    public class CollectibleManager : MonoBehaviour
    {
        private Camera _cam;

        private bool _isMultiple;
        
        private ICollectible _collectible;
        
        private void Start()
        {
            _cam = Camera.main;
        }
        
        private void Update()
        {
            _isMultiple = Input.GetKey(KeyCode.LeftShift);
            
            if (!Input.GetMouseButtonDown(0)) return;
            
            TryCollect();
        }

        private void TryCollect()
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity)) return;
            
            if (hit.collider == null) return;

            if (!hit.collider.TryGetComponent(out ICollectible collectible)) return;
            
            collectible.Collect(_isMultiple);
        }
    }
}