using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public Transform cameraTransform;

    public float interactionRange = 2.0f;
    public TextMeshProUGUI playerInteractText;

    void Update()
    {
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
