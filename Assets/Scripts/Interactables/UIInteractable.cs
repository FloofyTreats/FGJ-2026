using UnityEngine;

public class UIInteractable : Interactable
{
    public Canvas UI;
    public FirstPersonController controller;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip interactSound;
    public ControlAnimatedUI controlAnimatedUI;

    private void Start()
    {
        controller = FindAnyObjectByType<FirstPersonController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (controlAnimatedUI)
        {
            controlAnimatedUI.StopAnimation();
        }
    }

    public override void Interact()
    {
        controller.ToggleInUI();
        UI.enabled = true;
        if(interactSound)
        {
            audioSource.PlayOneShot(interactSound);
        }

        if(controlAnimatedUI)
        {
            controlAnimatedUI.StartAnimation();
        }
    }

    private void LateUpdate()
    {
        if (UI.enabled && Input.GetButtonDown("Cancel"))
        {
            UI.enabled = false;
            controller.ToggleInUI();

            if (controlAnimatedUI)
            {
                controlAnimatedUI.StopAnimation();
            }
        }
    }
}
