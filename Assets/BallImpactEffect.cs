using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpactEffect : MonoBehaviour
{
    public ParticleSystem dustEffect;
    public float impactForceThresh = 1;
    public float impactMultiplier = 1;
    public float offsetAmount = 1;
    CameraShake cameraShake;


    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        float impactForce = collision.relativeVelocity.magnitude;
        Debug.Log(impactForce);

        if (impactForce > impactForceThresh) // Adjust this threshold as needed
        {
            if (cameraShake != null) 
            {
                cameraShake.TriggerShake();
            }
            // The point of impact
            ContactPoint contact = collision.contacts[0];
            Vector3 offsetPosition = contact.point + (contact.normal * offsetAmount);
            dustEffect.transform.position = offsetPosition;

            int burstAmount = Mathf.RoundToInt(impactForce * impactMultiplier);
            dustEffect.Emit(burstAmount);
            // Adjust particle emission based on impact force
            //var emission = dustEffect.emission;
            //emission.SetBurst(0, new ParticleSystem.Burst(0, impactForce * impactMultiplier)); // Multiplying by 10 for more particles

            //var main = dustEffect.main;
            //main.startSpeed = impactForce;

            //dustEffect.Play();
        }
    }
}