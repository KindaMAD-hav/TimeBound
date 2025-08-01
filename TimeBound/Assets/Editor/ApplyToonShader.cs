using UnityEngine;
using UnityEditor;

public class ApplyToonShader : EditorWindow
{
    [MenuItem("Tools/Apply Toon Shader To All Materials")]
    public static void ApplyShader()
    {
        string shaderName = "SimpleURPToonLitExample(With Outline)";
        Shader toonShader = Shader.Find(shaderName);

        if (toonShader == null)
        {
            Debug.LogError("Shader not found: " + shaderName);
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:Material");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat != null)
            {
                mat.shader = toonShader;
                Debug.Log("Applied toon shader to: " + mat.name);
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Done applying toon shader to all materials.");
    }
}
