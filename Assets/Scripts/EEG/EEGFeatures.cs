using System;
using UnityEngine;

[Serializable]
public class EEGFeatures
{
    [Range(0f, 1f)] public float delta;
    [Range(0f, 1f)] public float theta;
    [Range(0f, 1f)] public float alpha;
    [Range(0f, 1f)] public float beta;
    [Range(0f, 1f)] public float gamma;

    public EEGFeatures(
        float delta = 0.5f,
        float theta = 0.5f,
        float alpha = 0.5f,
        float beta = 0.5f,
        float gamma = 0.5f)
    {
        this.delta = delta;
        this.theta = theta;
        this.alpha = alpha;
        this.beta = beta;
        this.gamma = gamma;
    }
}
