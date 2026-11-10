using UnityEngine;

public class FeatureProcessor : MonoBehaviour
{
    public EEGInputManager inputManager;

    [Range(0.1f, 10f)]
    public float smoothingSpeed = 2f;

    public EEGFeatures ProcessedFeatures { get; private set; }
        = new EEGFeatures();

    void Update()
    {
        if (inputManager == null)
            return;

        EEGFeatures input = inputManager.CurrentFeatures;

        float factor = 1f -
            Mathf.Exp(-smoothingSpeed * Time.deltaTime);

        ProcessedFeatures.delta = Mathf.Lerp(
            ProcessedFeatures.delta, input.delta, factor);

        ProcessedFeatures.theta = Mathf.Lerp(
            ProcessedFeatures.theta, input.theta, factor);

        ProcessedFeatures.alpha = Mathf.Lerp(
            ProcessedFeatures.alpha, input.alpha, factor);

        ProcessedFeatures.beta = Mathf.Lerp(
            ProcessedFeatures.beta, input.beta, factor);

        ProcessedFeatures.gamma = Mathf.Lerp(
            ProcessedFeatures.gamma, input.gamma, factor);
    }
}