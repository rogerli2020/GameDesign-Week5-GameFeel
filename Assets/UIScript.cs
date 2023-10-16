using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UIScript : MonoBehaviour
{
    public GameObject spherePrefab;
    public AudioSource ambientNoise;
    public Camera mainCamera;

    public PostProcessVolume ppvolume;

    private ColorGrading colorgrading = null;
    private DepthOfField dof = null;
    private Grain grain = null;
    private ChromaticAberration chromaticAbberation = null;

    private BallImpactEffect ballImpactScript;

    private bool sfxOn;

    private void Start()
    {
        GameObject sphereObject = GameObject.FindGameObjectWithTag("Sphere");
        Debug.Log(sphereObject);
        ballImpactScript = sphereObject.GetComponent<BallImpactEffect>();
        Debug.Log(ballImpactScript);

        ppvolume.profile.TryGetSettings(out colorgrading);
        ppvolume.profile.TryGetSettings(out dof);
        ppvolume.profile.TryGetSettings(out grain);
        ppvolume.profile.TryGetSettings(out chromaticAbberation);

        

        sfxOn = true;
    }

    public void ToggleSFX()
    {
        sfxOn = !sfxOn;
        foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("Sphere"))
        {
            BallSFXScript BallSFXController = sphere.GetComponent<BallSFXScript>();
            if (sfxOn)
            {
                BallSFXController.TurnOnSFX();
            } else
            {
                BallSFXController.TurnOffSFX();
            }
        }
    }

    public void ToggleParticlesEffect()
    {
        GameObject dustEffectObject = GameObject.Find("DustEffect");
        bool isOn = dustEffectObject.transform.localScale.x != 0;
        if (isOn)
        {
            dustEffectObject.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            dustEffectObject.transform.localScale = new Vector3(1, 1, 1);
        }


    }

    public void ToggleCameraShake()
    {
        mainCamera.GetComponent<CameraShake>().enabled = !mainCamera.GetComponent<CameraShake>().enabled;
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

    public void ToggleChromaticAberration()
    {
        chromaticAbberation.enabled.value = !chromaticAbberation.enabled.value;
        grain.enabled.value = !grain.enabled.value;
    }

    public void ToggleDOF()
    {
        dof.enabled.value = !dof.enabled.value;
    }

    public void ToggleColorGrading()
    {
        colorgrading.enabled.value = !colorgrading.enabled.value;
    }

    public void ToggleCracking()
    {
        ballImpactScript.ToggleCracking();
    }


    public void SpawnBall()
    {
        GameObject newSphere = Instantiate(spherePrefab);
        if (!sfxOn)
        {
            newSphere.GetComponent<BallSFXScript>().TurnOffSFX();
            ballImpactScript = newSphere.GetComponent<BallImpactEffect>();
        }
    }
}
