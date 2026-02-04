using UnityEngine;
using UnityEditor;
using Task.Data;

namespace Task.EditorTools
{
    /// <summary>
    /// Add a button to force update scene name references
    /// </summary>
    [CustomEditor(typeof(SceneReferences))]
    public class SceneReferencesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SceneReferences script = (SceneReferences)target;

            GUILayout.Space(10);

            if(GUILayout.Button("Force Update Names"))
            {
                script.ValidateData();
                AssetDatabase.SaveAssets();
                Debug.Log("Scene names updated manually.");
            }
        }
    }

    
}