using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactText = "Press E to interact";
    public abstract void Interact();
}