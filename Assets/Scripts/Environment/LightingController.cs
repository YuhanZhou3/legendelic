using UnityEngine;

public class LightingController : MonoBehaviour
{
    public Light[] lights;

    public float minIntensity = 0.3f;
    public float maxIntensity = 2f;

    public float minFogDensity = 0.005f;
    public float maxFogDensity = 0.04f;

    public void SetIntensity(float value)
    {
        value = Mathf.Clamp01(value);

        float intensity = Mathf.Lerp(
            minIntensity,
            maxIntensity,
            value);

        foreach (Light lightSource in lights)
        {
            if (lightSource != null)
                lightSource.intensity = intensity;
        }
    }

    public void SetFog(float value)
    {
        RenderSettings.fog = true;

        RenderSettings.fogDensity = Mathf.Lerp(
            minFogDensity,
            maxFogDensity,
            Mathf.Clamp01(value));
    }
}