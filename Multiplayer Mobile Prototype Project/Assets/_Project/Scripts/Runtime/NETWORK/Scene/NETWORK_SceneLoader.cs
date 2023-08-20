using Photon.Pun;

namespace Core.Runtime.NETWORK.Scene
{
    public static class NETWORK_SceneLoader
    {
        public static void LoadScene(string sceneName)
        {
            if (!Init()) return;
            
            PhotonNetwork.LoadLevel(sceneName);
        }

        public static void LoadScene(int sceneIndex)
        {
            if (!Init()) return;
            
            PhotonNetwork.LoadLevel(sceneIndex);
        }

        private static bool Init()
        {
            return PhotonNetwork.IsMasterClient;
        }
    }
}