using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    public VisualEffect particleField;

    public float minParticleSpeed = 0.2f;
    public float maxParticleSpeed = 3f;

    public float minParticleIntensity = 10f;
    public float maxParticleIntensity = 100f;

    public void SetParameters(float speed, float intensity)
    {
        if (particleField == null)
            return;

        float actualSpeed = Mathf.Lerp(
            minParticleSpeed,
            maxParticleSpeed,
            speed);

        float actualIntensity = Mathf.Lerp(
            minParticleIntensity,
            maxParticleIntensity,
            intensity);

        particleField.SetFloat("ParticleSpeed", actualSpeed);
        particleField.SetFloat("ParticleIntensity", actualIntensity);
    }
}