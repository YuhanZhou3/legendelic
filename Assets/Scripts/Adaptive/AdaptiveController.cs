using UnityEngine;

public class AdaptiveController : MonoBehaviour
{
    public FeatureProcessor processor;
    public EnvironmentController environment;

    void Update()
    {
        if (processor == null || environment == null)
            return;

        EEGFeatures f = processor.ProcessedFeatures;

        EnvironmentParameters parameters =
            new EnvironmentParameters();

        parameters.particleSpeed = f.alpha;
        parameters.particleIntensity = f.gamma;
        parameters.waveAmplitude = f.theta;
        parameters.lightIntensity = f.beta;
        parameters.fogDensity = f.delta;

        environment.ApplyParameters(parameters);
    }
}