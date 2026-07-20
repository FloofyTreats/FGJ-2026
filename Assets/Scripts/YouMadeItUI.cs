using UnityEngine;

public class YouMadeItUI : MonoBehaviour
{
    public FirstPersonController controller;

    private Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = FindAnyObjectByType<FirstPersonController>();
        canvas = GetComponent<Canvas>();
    }

    public void CloseMenu()
    {
        controller.ToggleInUI();
        canvas.enabled = false;
    }
}
