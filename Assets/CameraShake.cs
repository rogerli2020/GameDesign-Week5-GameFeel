using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public float dampingSpeed = 1.0f;
    private Vector3 initialPosition;
    private float currentShakeDuration = 0f;
    private bool isShaking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (isShaking)
        {
            if (currentShakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                currentShakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                isShaking = false;
                currentShakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }
    }

    public void TriggerShake()
    {
        isShaking = true;
        currentShakeDuration = shakeDuration;
        initialPosition = transform.localPosition;
    }
}
