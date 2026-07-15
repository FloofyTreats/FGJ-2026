using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}
