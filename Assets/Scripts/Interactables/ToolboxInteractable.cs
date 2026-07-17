using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class ToolboxInteractable : UIInteractable
{
    public bool isRadioTaken = false;
    public SpriteRenderer manualSprite;
    public SpriteRenderer openSprite;
    public SpriteRenderer closeSprite;
    public Canvas toolboxCanvas;
    public Canvas manualCanvas;
    public Image toolboxOpen;
    public Image toolboxClose;
    public Image manualOpen;
    public Image manualClose;

    private int currentStep = 0;

    public void nextStep()
    {
        currentStep++;
        if(currentStep > 4)
        {
            currentStep = 4;
        }

        switch(currentStep)
        {
            case 1:
                toolboxOpen.enabled = true;
                toolboxClose.enabled = false;
                break;
            case 2:
                toolboxCanvas.enabled = false;
                manualCanvas.enabled = true;
                break;
            case 3:
                manualClose.enabled = false;
                manualOpen.enabled = true;
                break;
            case 4:
                isRadioTaken = true;
                StartCoroutine(controller.displayCaption("Press ? to use the Sonic Radio", 5f));
                UI.enabled = false;
                manualSprite.enabled = true;
                openSprite.enabled = true;
                closeSprite.enabled = false;
                controller.ToggleInUI();
                break;
        }
        
    }

    public void resetStep()
    {
        currentStep = 0;
    }
}
