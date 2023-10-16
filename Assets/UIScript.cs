using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject spherePrefab;
    public AudioSource ambientNoise;

    public void ToggleSFX()
    {
        foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("Sphere"))
        {
            BallSFXScript BallSFXController = sphere.GetComponent<BallSFXScript>();
            if (BallSFXController.isOn)
            {
                BallSFXController.TurnOffSFX();
            } else
            {
                BallSFXController.TurnOnSFX();
            }
        }
    }

    public void ToggleParticlesEffect()
    {

    }

    public void ToggleCameraShake()
    {

    }


    public void ToggleAmbientNoise()
    {
        bool isOff = ambientNoise.volume == 0.0f;
        if (isOff)
        {
            ambientNoise.volume = 1.0f;
        } else
        {
            ambientNoise.volume = 0.0f;
        }
    }


    public void SpawnBall()
    {
        Instantiate(spherePrefab);
    }
}
