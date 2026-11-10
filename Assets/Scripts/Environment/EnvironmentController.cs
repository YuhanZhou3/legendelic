using UnityEngine;

[System.Serializable]
public class EnvironmentParameters
{
    [Range(0f, 1f)] public float particleSpeed;
    [Range(0f, 1f)] public float particleIntensity;
    [Range(0f, 1f)] public float waveAmplitude;
    [Range(0f, 1f)] public float lightIntensity;
    [Range(0f, 1f)] public float fogDensity;
}

public class EnvironmentController : MonoBehaviour
{
    public VFXController vfx;
    public LightingController lighting;
    public GeometryAnimator geometry;
    public PostProcessingController postProcessing;

    public void ApplyParameters(EnvironmentParameters p)
    {
        if (vfx != null) {
            vfx.SetParameters(
                p.particleSpeed,
                p.particleIntensity);
        }

        if (lighting != null) {
            lighting.SetIntensity(p.lightIntensity);
            lighting.SetFog(p.fogDensity);
        }

        if (geometry != null)
            geometry.SetAmplitude(p.waveAmplitude);

        if (postProcessing != null) {
            postProcessing.SetParameters(
                p.particleIntensity,
                p.waveAmplitude);
        }
    }
}