using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    public Volume volume;

    private Bloom bloom;
    private ChromaticAberration chromatic;

    void Start()
    {
        if (volume == null)
            return;

        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out chromatic);
    }

    public void SetParameters(float intensity, float distortion)
    {
        if (bloom != null)
        {
            bloom.intensity.value = Mathf.Lerp(
                0.2f, 2f, intensity);
        }

        if (chromatic != null)
        {
            chromatic.intensity.value = Mathf.Lerp(
                0f, 0.15f, distortion);
        }
    }
}