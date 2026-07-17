using UnityEngine;

public class UIInteractable : Interactable
{
    public Canvas UI;
    public FirstPersonController controller;
    public Animator animator;
    public AudioSource audioSource;

    private void Start()
    {
        controller = FindAnyObjectByType<FirstPersonController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        controller.ToggleInUI();
        UI.enabled = true;
    }

    private void Update()
    {
        if (UI.enabled && Input.GetButtonDown("Cancel"))
        {
            UI.enabled = false;
            controller.ToggleInUI();
        }
    }
}
