using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorInteractable : Interactable
{
    public string nextLevel;
    public bool locked = true;
    public RadioFrequencyController frequencyController;
    public VolumeController volumeController;
    public string validFrequency;
    public float validVolume;
    public AudioClip unlockSound;
    public AudioClip openSound;

    private FirstPersonController controller;
    private ScreenFade screenFade;
    private AudioSource audioSource;

    private void Start()
    {
        controller = FindAnyObjectByType<FirstPersonController>();
        screenFade = FindAnyObjectByType<ScreenFade>();
        audioSource = GetComponent<AudioSource>();
    }
    public override void Interact()
    {
        if( locked )
        {
            StartCoroutine(controller.displayCaption("It's locked.", 3f));
        }
        else
        {
            StartCoroutine("ChangeLevel");
        }
    }

    public void CheckRadioParameters()
    {
        if(!Level1Manager.Instance.soundManipulationController.powered || !locked)
        {
            return;
        }
        bool validValues = frequencyController.Frequency.ToString("F1") == validFrequency && Mathf.Abs(validVolume - volumeController.Volume) < 0.05f;

        Debug.Log("Volume: " + volumeController.Volume + ", Frequency: " + frequencyController.Frequency.ToString("F1"));

        ToggleDoorLock(!validValues);
    }

    public void ToggleDoorLock(bool val)
    {
        locked = val;
        if(!locked)
        {
            audioSource.PlayOneShot(unlockSound);
        }
    }

    IEnumerator ChangeLevel()
    {
        audioSource.PlayOneShot(openSound);
        controller.LockMovement();
        screenFade.FadeOut();

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(nextLevel);
    }
}
