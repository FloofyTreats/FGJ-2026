using UnityEngine;

public class NormalDoorInteractable : Interactable
{
    public bool unlocked = false;
    public bool open = false;

    private AudioSource audioSource;

    private FirstPersonController controller;
    private MeshRenderer meshRenderer;
    private BoxCollider collider;

    private void Start()
    {
        controller = FindAnyObjectByType<FirstPersonController>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }

    public override void Interact()
    {
        if (!unlocked)
        {
            StartCoroutine(controller.displayCaption("It's locked.", 3.0f));
        }
        else
        {
            audioSource.Play();

            meshRenderer.enabled = false;
            collider.enabled = false;
        }
    }
}
