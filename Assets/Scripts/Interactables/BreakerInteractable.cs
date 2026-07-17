using UnityEngine;
using UnityEngine.UI;

public class BreakerInteractable : UIInteractable
{
    public BreakerDial[] dials;
    public string correctCombination;
    public Canvas lockCanvas;

    [Header("World sprites")]
    public GameObject breakerLockSprite;
    public SpriteRenderer breakerLeverDownSprite;
    public SpriteRenderer breakerLeverUpSprite;

    [Header("UI images")]
    public Image breakerLeverDownUI;
    public Image breakerLeverUpUI;
    public UISpriteAnimation lockUIAnimation;
    public Sprite[] unlockedSprites;

    [Header("Sounds")]
    public AudioClip powerSound;
    public AudioClip wrongSound;
    public AudioClip unlockSound;

    private bool unlocked = false;
    private bool powerOn = false;

    public void CheckCombination()
    {
        string currentCombination = "";
        foreach (var dial in dials)
        {
            currentCombination += dial.GetCurrentValue();
        }

        if (currentCombination == correctCombination)
        {
            UnlockBreaker();
        }
        else
        {
            audioSource.PlayOneShot(wrongSound);
        }
    }

    public void UnlockBreaker()
    {
        lockUIAnimation.m_SpriteArray = unlockedSprites;
        audioSource.PlayOneShot(unlockSound);
        animator.Play("lock_fall");
        unlocked = true;
        breakerLockSprite.SetActive(false);

        breakerLeverDownUI.raycastTarget = true;
    }

    public void HideLock()
    {
        lockCanvas.enabled = false;
    }

    public void TurnPowerOn()
    {
        if (powerOn)
        {
            return;
        }

        powerOn = true;
        audioSource.PlayOneShot(powerSound);

        breakerLeverDownUI.enabled = false;
        breakerLeverUpUI.enabled = true;

        breakerLeverDownSprite.enabled = false;
        breakerLeverUpSprite.enabled = true;

        Level1Manager.Instance.TurnPowerOn();
    }
}
