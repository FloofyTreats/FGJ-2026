using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class PhoneInteractable : UIInteractable
{
    public string correctSequence;
    public AudioClip[] buttonSounds;
    public TextMeshProUGUI firstPart;
    public TextMeshProUGUI secondPart;
    public TextMeshProUGUI thirdPart;

    private string currentSequence = "";
    private bool isLockedIn = false;
    private readonly Dictionary<string, int> buttonValueToSoundIndex = new()
    {
        {"1", 0},
        {"2", 1},
        {"3", 2},
        {"4", 3},
        {"5", 4},
        {"6", 5},
        {"7", 6},
        {"8", 7},
        {"9", 8},
        {"*", 9},
        {"0", 10},
        {"#", 11},
    };
    public void PhoneButtonPressed(string value)
    {
        if (isLockedIn) {
            StartCoroutine(controller.displayCaption("Can't call. The number is stuck on screen.", 5f));
            return;
        }

        if (!Level1Manager.Instance.powerOn)
        {
            StartCoroutine(controller.displayCaption("No power. Phone doesn't work.", 5f));
        }
        else
        {
            int sequenceLen = currentSequence.Length;

            if(sequenceLen < 2)
            {
                firstPart.text += value;
            }
            else if(sequenceLen < 4)
            {
                secondPart.text += value;
            }
            else
            {
                thirdPart.text += value;
            }

            int buttonIndex = buttonValueToSoundIndex[value];
            audioSource.PlayOneShot(buttonSounds[buttonIndex]);
            currentSequence += value;

            if (currentSequence.Length == 6) {
                if(currentSequence == correctSequence)
                {
                    LockSequence();
                }
                else
                {
                    currentSequence = "";
                    firstPart.text = "";
                    secondPart.text = "";
                    thirdPart.text = "";
                    sequenceLen = 0;
                }
            }
        }
    }

    public void LockSequence()
    {
        StartCoroutine(PlayCorrectNotes());
        isLockedIn = true;
    }

    IEnumerator PlayCorrectNotes()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(buttonSounds[1]);
        yield return new WaitForSeconds(0.3f);
        audioSource.PlayOneShot(buttonSounds[8]);
        yield return new WaitForSeconds(0.3f);
        audioSource.PlayOneShot(buttonSounds[3]);
        yield return new WaitForSeconds(0.3f);
        audioSource.PlayOneShot(buttonSounds[8]);
        yield return new WaitForSeconds(0.3f);
        audioSource.PlayOneShot(buttonSounds[10]);
        yield return new WaitForSeconds(0.3f);
        audioSource.PlayOneShot(buttonSounds[3]);
    }
}
