using System;
using UnityEngine;

namespace Core.Runtime.NETWORK.Instantiate
{
    [Serializable]
    public class NetworkPrefab
    {
        public GameObject Prefab;
        public string Path;

        public NetworkPrefab(GameObject prefab, string path)
        {
            Prefab = prefab;
            Path = GetModifiedPath(path);
        }

        private static string GetModifiedPath(string path)
        {
            var extensionLenght = System.IO.Path.GetExtension(path).Length;

            const int additionalLenght = 10;

            var startIndex = path.ToLower().IndexOf("resources", StringComparison.Ordinal) + additionalLenght;

            return startIndex == -1
                ? string.Empty
                : path.Substring(startIndex, path.Length - (startIndex + extensionLenght));
        }
    }
}