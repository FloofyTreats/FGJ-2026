using UnityEditor.Animations;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DialData {
    public DialData(string val, Sprite[] fram)
    {
        Value = val;
        Frames = fram;
    }

    public string Value;
    public Sprite[] Frames;
}

public class BreakerDial : MonoBehaviour
{
    public DialData[] dialDatas;
    public int currentIndex;
    public AudioClip dialSound;

    private Image image;
    private UISpriteAnimation uiSpriteAnimation;
    private AudioSource audioSource;

    private void Start()
    {
        image = GetComponent<Image>();
        uiSpriteAnimation = GetComponent<UISpriteAnimation>();
        audioSource = GetComponent<AudioSource>();
    }

    public void NextData()
    {
        currentIndex++;
        if (currentIndex >= dialDatas.Length)
        {
            currentIndex = 0;
        }

        audioSource.PlayOneShot(dialSound);
        uiSpriteAnimation.m_SpriteArray = dialDatas[currentIndex].Frames;
    }

    public void PreviousData()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = dialDatas.Length - 1;
        }

        audioSource.PlayOneShot(dialSound);
        uiSpriteAnimation.m_SpriteArray = dialDatas[currentIndex].Frames;
    }

    public string GetCurrentValue()
    {
        return dialDatas[currentIndex].Value;
    }
}
