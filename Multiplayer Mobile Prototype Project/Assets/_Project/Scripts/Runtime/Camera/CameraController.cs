using UnityEngine;

namespace Core.Runtime.Camera
{
    public class CameraController : MonoBehaviour
    {
        
        [Header("Setup")]
        [SerializeField] private Vector3 _lookDir;
        [SerializeField] private Transform _ctx;
        
        [Header("Settings")]
        [SerializeField] private float _movementSmoothness = .001f;
        [SerializeField] private float _panSpeed = 20f;

        
        private Vector3 _velocity;

        private float _horizontalAxis;
        private float _verticalAxis;

        private void Update()
        {
            InputHandler();
        }
        
        private void FixedUpdate()
        {
            PositionHandler();
            RotationHandler();
        }

        private void InputHandler()
        {
            _horizontalAxis = Input.GetAxis("Horizontal");
            _verticalAxis = Input.GetAxis("Vertical");
        }
        
        private void PositionHandler()
        {
            var pos = _ctx.position;

            MovementHandler(ref pos);

            _ctx.position = Vector3.SmoothDamp(_ctx.position, pos, ref _velocity, _movementSmoothness);
        }

        private void MovementHandler(ref Vector3 pos)
        {
            var forward = _ctx.forward;
            forward.y = 0f;
            
            var right = _ctx.right;
            right.y = 0f;
            
            pos += forward * (_verticalAxis   * _panSpeed * Time.fixedDeltaTime);
            pos += right   * (_horizontalAxis * _panSpeed * Time.fixedDeltaTime);
        }
        
        private void RotationHandler()
        {
            _ctx.rotation = Quaternion.Euler(_lookDir);
        }
    }
}