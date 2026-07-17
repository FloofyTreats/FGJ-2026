using UnityEngine;

public class MicrowaveButton : MonoBehaviour
{
    public bool active = false;
    public Sprite[] inactiveSprites;
    public Sprite[] activeSprites;

    private UISpriteAnimation spriteAnimation;

    public void ToggleActive()
    {
        active = !active;

        spriteAnimation.m_SpriteArray = active ? activeSprites : inactiveSprites;
    }
}
