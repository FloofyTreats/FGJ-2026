using UnityEngine;
using UnityEngine.UI;

public class SafeInteractable : UIInteractable
{
    public string[] validFrequencies;
    public bool firstHit = false;
    public bool secondHit = false;
    public bool thirdHit = false;

    public SpriteRenderer safeDoorClosedSprite;
    public SpriteRenderer safeDoorOpenSprite;
    public Image safeDoorClosedUI;
    public Image safeDoorOpenUI;
}
