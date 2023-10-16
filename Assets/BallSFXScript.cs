using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSFXScript : MonoBehaviour
{
    public AudioClip hitGroundSFX;
    public AudioSource hitGroundAudioSource;
    public AudioSource rollingAudioSource;
    public bool isOn;

    private void Start()
    {
        isOn = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rollingAudioSource.Play();
        hitGroundAudioSource.PlayOneShot(hitGroundSFX, 1.0f);
    }

    private void OnCollisionExit(Collision collision)
    {
        rollingAudioSource.Stop();
    }

    public void TurnOnSFX()
    {
        rollingAudioSource.volume = 1f;
        hitGroundAudioSource.volume = 1f;
        isOn = true;
    }

    public void TurnOffSFX()
    {
        rollingAudioSource.volume = 0f;
        hitGroundAudioSource.volume = 0f;
        isOn = false;
    }
}
