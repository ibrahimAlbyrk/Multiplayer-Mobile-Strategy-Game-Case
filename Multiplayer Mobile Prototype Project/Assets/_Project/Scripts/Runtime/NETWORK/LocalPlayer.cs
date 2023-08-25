using UnityEngine;

namespace Core.Runtime.NETWORK
{
    public static class LocalPlayer
    {
        private static Color _color;

        public static void SetColor(float[] colorArray) =>
            _color = new Color(colorArray[0], colorArray[1], colorArray[2]);
        
        public static Color GetColor() => _color;
    }
}