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
    public GameObject crackSpritePrefab;
    public float minScale = 1f;
    public float maxScale = 3f;
    private bool crackingEnabled = true;


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


            if (collision.gameObject.CompareTag("Impactable") && crackingEnabled)
            {
                Vector3 impactPoint = contact.point;
                Quaternion impactRotation = Quaternion.LookRotation(collision.contacts[0].normal, Vector3.up);
                impactRotation *= Quaternion.Euler(90f, 0f, 0f);
                float scaleValue = Mathf.Clamp(impactForce * impactMultiplier, minScale, maxScale);

                GameObject crackInstance = Instantiate(crackSpritePrefab, impactPoint, impactRotation);
                crackInstance.transform.localScale = new Vector3(scaleValue, scaleValue, 1f);
                crackInstance.transform.Translate(Vector3.up * 0.1f);
                crackInstance.transform.Translate(new Vector3(0, 1, 0) * 1f);
            }
            
            // Adjust particle emission based on impact force
            //var emission = dustEffect.emission;
            //emission.SetBurst(0, new ParticleSystem.Burst(0, impactForce * impactMultiplier)); // Multiplying by 10 for more particles

            //var main = dustEffect.main;
            //main.startSpeed = impactForce;

            //dustEffect.Play();
        }
    }

    public void ToggleCracking() {
        crackingEnabled = !crackingEnabled;
    }
}