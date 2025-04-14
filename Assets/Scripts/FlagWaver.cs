using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagWaver : MonoBehaviour
{
    public float maxAngle = 10f;          // Max angle
    public float waveFrequency = 0.5f;    // Oscilations per second
    public float randomAmplitude = 2f;    // Random angle variation

    private float baseAngle;
    private float timeOffset;

    void Start()
    {
        baseAngle = transform.localEulerAngles.y;
        timeOffset = Random.Range(0f, 100f);
    }

    void FixedUpdate()
    {
        // Movement
        float wave = Mathf.Sin((Time.time + timeOffset) * waveFrequency * 2 * Mathf.PI) *
                     (maxAngle + Random.Range(-randomAmplitude, randomAmplitude));

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            baseAngle + wave,
            transform.localEulerAngles.z
        );
    }
}
