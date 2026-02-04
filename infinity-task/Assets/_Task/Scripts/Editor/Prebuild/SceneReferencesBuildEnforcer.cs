using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Task.Data;

namespace Task.Prebuild
{
    /// <summary>
    /// redo scene references before building
    /// </summary>
    public class SceneReferencesBuildEnforcer : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("Build Started: Enforcing Scene Names...");

            string[] guids = AssetDatabase.FindAssets("t:SceneReferences");

            foreach(string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SceneReferences data = AssetDatabase.LoadAssetAtPath<SceneReferences>(path);

                if(data != null)
                {
                    data.ValidateData();
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}