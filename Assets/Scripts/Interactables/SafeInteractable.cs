using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class SafeInteractable : UIInteractable
{
    public string[] validFrequencies;
    private bool firstHit = false;
    private bool secondHit = false;
    private bool thirdHit = false;
    private bool unlocked = false;
    private bool open = false;
    private bool metronomeGet = false;

    [Header("Overworld")]
    public SpriteRenderer safeDoorClosedSprite;
    public SpriteRenderer safeDoorOpenSprite;
    public SpriteRenderer metronomeUpgradeSprite;
    public GameObject lightOff1;
    public GameObject lightOn1;
    public GameObject lightOff2;
    public GameObject lightOn2;
    public GameObject lightOff3;
    public GameObject lightOn3;

    [Header("UI")]
    public Image safeDoorClosedUI;
    public Image safeDoorOpenUI;
    public Image metronomeUpgradeUI;
    public UISpriteAnimation light1UI;
    public UISpriteAnimation light2UI;
    public UISpriteAnimation light3UI;

    [Header("Sprites")]
    public Sprite[] lightOffSprites;
    public Sprite[] lightOnSprites;

    [Header("Sounds")]
    public AudioClip hitSound;
    public AudioClip unlockSound;
    public AudioClip openSound;

    public void CheckFrequencyHit(string frequency)
    {
        if(unlocked)
        {
            return;
        }

        bool validHit = false;

        if(firstHit == false)
        {
            validHit = validFrequencies[0] == frequency;
            firstHit = validHit;
        }
        else if (secondHit == false)
        {
            validHit = validFrequencies[1] == frequency;
            secondHit = validHit;
        }
        else
        {
            validHit = validFrequencies[2] == frequency;
            thirdHit = validHit;
        }

        if (!validHit)
        {
            ResetLights();
        }
        else
        {
            int whichHit = thirdHit == true ? 3 : (secondHit == true ? 2 : 1);
            SetLight(whichHit);
        }

        if(firstHit && secondHit && thirdHit)
        {
            UnlockSafe();
        }
    }

    public void ResetLights()
    {
        lightOff1.SetActive(true);
        lightOff2.SetActive(true);
        lightOff3.SetActive(true);
        lightOn1.SetActive(false);
        lightOn2.SetActive(false);
        lightOn3.SetActive(false);

        light1UI.m_SpriteArray = lightOffSprites;
        light2UI.m_SpriteArray = lightOffSprites;
        light3UI.m_SpriteArray = lightOffSprites;
    }

    public void SetLight(int whichHit)
    {
        audioSource.PlayOneShot(hitSound);
        if (whichHit == 1)
        {
            lightOff1.SetActive(false);
            lightOn1.SetActive(true);

            light1UI.m_SpriteArray = lightOnSprites;
        }
        else if (whichHit == 2) {
            lightOff2.SetActive(false);
            lightOn2.SetActive(true);

            light2UI.m_SpriteArray = lightOnSprites;
        }
        else
        {
            lightOff3.SetActive(false);
            lightOn3.SetActive(true);

            light3UI.m_SpriteArray = lightOnSprites;
        }
    }

    public void UnlockSafe()
    {
        unlocked = true;
        safeDoorClosedUI.raycastTarget = true;
        audioSource.PlayOneShot(unlockSound);
    }

    public void OpenSafe()
    {
        if(open)
        {
            return;
        }

        open = true;
        audioSource.PlayOneShot(openSound);

        safeDoorClosedSprite.enabled = false;
        safeDoorOpenSprite.enabled = true;

        safeDoorClosedUI.enabled = false;
        safeDoorOpenUI.enabled = true;

        metronomeUpgradeSprite.enabled = true;
        metronomeUpgradeUI.enabled = true;
    }

    public void MetronomeUpgradeGet()
    {
        if(metronomeGet)
        {
            return;
        }

        metronomeGet = true;

        UI.enabled = false;

        if (controlAnimatedUI)
        {
            controlAnimatedUI.StopAnimation();
        }

        Level1Manager.Instance.GetUpgrade(1);
    }
}
