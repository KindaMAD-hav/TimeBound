using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Light directionalLight;
    [SerializeField] LightingPresets preset;
    [SerializeField] TimerMain timerScript; 

    //private
    private float time;

    private void Update()
    {
        if(preset == null)
        {
            return;
        }
        if(Application.isPlaying )
        {
            time = timerScript.currTime;
            updateLighting(time / (24*60));
        }
        else
        {
            updateLighting(time / (24*60));
        }
    }

    private void updateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambinetColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        if(directionalLight != null)
        {
            directionalLight.color = preset.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if(directionalLight != null) return;
        if(RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsByType<Light>(FindObjectsSortMode.None);
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    directionalLight = light;
                }
            }
        }
    }
}
