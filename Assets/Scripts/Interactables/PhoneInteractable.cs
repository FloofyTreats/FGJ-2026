using UnityEngine;
using System.Collections.Generic;

public class PhoneInteractable : UIInteractable
{
    public string correctSequence;
    public AudioClip[] buttonSounds;
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
            StartCoroutine(controller.displayCaption("Can't change the number anymore.", 5f));
            return;
        }

        if (!Level1Manager.Instance.powerOn)
        {
            StartCoroutine(controller.displayCaption("No power. Phone doesn't work.", 5f));
        }
        else
        {
            if (currentSequence.Length == 6) {
                currentSequence = "";
            }

            int buttonIndex = buttonValueToSoundIndex[value];
            audioSource.PlayOneShot(buttonSounds[buttonIndex]);
            currentSequence += value;

            if (currentSequence.Length == 6 && currentSequence == correctSequence) {
                LockSequence();
            }
        }
    }

    public void LockSequence()
    {
        isLockedIn = true;
    }
}
