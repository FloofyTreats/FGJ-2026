using UnityEngine;
using UnityEngine.UI;

public class RadioPowerSwitch : MonoBehaviour
{
    private bool on = false;

    public Sprite[] onSprites;
    public Sprite[] offSprites;
    public UISpriteAnimation switchImage;

    public void ChangePowerSprites()
    {
        on = !on;

        switchImage.m_SpriteArray = on ? onSprites : offSprites;
    }
}
