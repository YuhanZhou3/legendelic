using UnityEngine;

public class GeometryAnimator : MonoBehaviour
{
    public Transform[] objects;

    public float rotationSpeed = 15f;
    public float baseAmplitude = 0.1f;
    public float maxAmplitude = 1f;

    private Vector3[] initialPositions;
    private Vector3[] initialScales;

    private float amplitude = 0.5f;

    void Start()
    {
        initialPositions = new Vector3[objects.Length];
        initialScales = new Vector3[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            initialPositions[i] = objects[i].localPosition;
            initialScales[i] = objects[i].localScale;
        }
    }

    void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == null)
                continue;

            float phase = Time.time + i * 0.8f;

            objects[i].Rotate(
                Vector3.up,
                rotationSpeed * Time.deltaTime);

            float movement = Mathf.Sin(phase) *
                Mathf.Lerp(baseAmplitude, maxAmplitude, amplitude);

            objects[i].localPosition =
                initialPositions[i] +
                Vector3.up * movement;

            float scaleFactor =
                1f + 0.1f * amplitude * Mathf.Sin(phase);

            objects[i].localScale =
                initialScales[i] * scaleFactor;
        }
    }

    public void SetAmplitude(float value)
    {
        amplitude = Mathf.Clamp01(value);
    }
}