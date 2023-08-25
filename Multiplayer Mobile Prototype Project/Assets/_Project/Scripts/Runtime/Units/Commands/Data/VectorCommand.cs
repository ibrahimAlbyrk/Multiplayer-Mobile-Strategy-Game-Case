using UnityEngine;

namespace Core.Runtime.Units.Commands
{
    public class VectorCommand
    {
        private readonly float x;
        private readonly float y;
        private readonly float z;

        public Vector2 Vector2 => new (x, y);
        public Vector3 Vector3 => new (x, y, z);
        
        public VectorCommand(Vector3 vec)
        {
            x = vec.x;
            y = vec.y;
            z = vec.z;
        }
        
        public VectorCommand(Vector2 vec)
        {
            x = vec.x;
            y = vec.y;
        }
    }
}