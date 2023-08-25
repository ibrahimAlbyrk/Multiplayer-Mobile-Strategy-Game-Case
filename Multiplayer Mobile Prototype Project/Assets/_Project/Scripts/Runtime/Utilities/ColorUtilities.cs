using UnityEngine;

namespace Core.Runtime.Utilities
{
    public static class ColorUtilities
    {
        public static float ColorSimilarity(Color c1, Color c2)
        {
            var redDiff = c1.r - c2.r;
            var greenDiff = c1.g - c2.g;
            var blueDiff = c1.b - c2.b;

            var colorDistance = Mathf.Sqrt(redDiff * redDiff
                                           + greenDiff * greenDiff
                                           + blueDiff * blueDiff);

            return colorDistance;
        }
    }
}