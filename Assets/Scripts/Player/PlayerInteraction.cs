using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public Transform cameraTransform;
    public FirstPersonController controller;

    public float interactionRange = 2.0f;
    public TextMeshProUGUI playerInteractText;

    private void Start()
    {
        controller = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        if(controller.inUI)
        {
            playerInteractText.text = "";
            return;
        }

        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, interactionRange))
        {
            if (hit.collider.gameObject.TryGetComponent(out Interactable interactObj))
            {
                playerInteractText.text = interactObj.interactText;
                if (Input.GetButtonDown("Interact"))
                {
                    interactObj.Interact();
                }
            }
            else
            {
                playerInteractText.text = "";
            }
        }
        else
        {
            playerInteractText.text = "";
        }
    }
}
