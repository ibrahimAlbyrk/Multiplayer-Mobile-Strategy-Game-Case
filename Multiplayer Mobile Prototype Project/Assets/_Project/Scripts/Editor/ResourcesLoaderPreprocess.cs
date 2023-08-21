using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Core.Editor
{
    using Runtime.NETWORK.Instantiate;
    
    public class ResourcesLoaderPreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            NetworkInstantiater.LoadNetworkPrefabs();
        }
    }
}