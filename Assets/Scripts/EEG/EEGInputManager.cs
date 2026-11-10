using UnityEngine;

public class EEGInputManager : MonoBehaviour
{
    public bool automaticMode = true;

    public EEGFeatures CurrentFeatures { get; private set; }
        = new EEGFeatures();

    void Update()
    {
        if (automaticMode)
            GenerateSyntheticFeatures();
    }

    void GenerateSyntheticFeatures()
    {
        float t = Time.time;

        CurrentFeatures.delta =
            0.5f + 0.35f * Mathf.Sin(t * 0.20f);

        CurrentFeatures.theta =
            0.5f + 0.35f * Mathf.Sin(t * 0.35f + 1f);

        CurrentFeatures.alpha =
            0.5f + 0.35f * Mathf.Sin(t * 0.25f + 2f);

        CurrentFeatures.beta =
            0.5f + 0.35f * Mathf.Sin(t * 0.45f + 3f);

        CurrentFeatures.gamma =
            0.5f + 0.35f * Mathf.Sin(t * 0.30f + 4f);
    }

    public void SetAutomaticMode(bool enabled)
    {
        automaticMode = enabled;
    }

    public void SetManualFeature(string feature, float value)
    {
        if (automaticMode)
            return;

        value = Mathf.Clamp01(value);

        switch (feature.ToLower())
        {
            case "delta":
                CurrentFeatures.delta = value;
                break;

            case "theta":
                CurrentFeatures.theta = value;
                break;

            case "alpha":
                CurrentFeatures.alpha = value;
                break;

            case "beta":
                CurrentFeatures.beta = value;
                break;

            case "gamma":
                CurrentFeatures.gamma = value;
                break;
        }
    }
}
