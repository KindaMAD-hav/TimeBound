using UnityEditor;
using UnityEngine;

public class ApplyToonShaderWithOutline : EditorWindow
{
    [MenuItem("Tools/Apply Toon Outline Shader To All Materials")]
    public static void ApplyShader()
    {
        string shaderName = "Custom/ToonLitOutlineURP";
        Shader toonShader = Shader.Find(shaderName);

        if (toonShader == null)
        {
            Debug.LogError("Shader not found: " + shaderName);
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:Material");
        int count = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat != null)
            {
                mat.shader = toonShader;
                count++;
            }
        }

        Debug.Log($"Toon Outline Shader applied to {count} materials.");
    }
}
