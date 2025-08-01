using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="Lighting Preset",menuName = "Scriptables/Lighting Preset",order = 1)]
public class LightingPresets : ScriptableObject
{
    public Gradient ambinetColor;
    public Gradient directionalColor;
    public Gradient fogColor;
}
