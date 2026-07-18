using UnityEngine;
using UnityEngine.InputSystem;

public class PointerVisualMouseControl : MonoBehaviour
{
    [SerializeField] RectTransform myTransform;
    [SerializeField] GameObject handPointerImage;
    [SerializeField] Camera canvasCamera;
    [SerializeField] RectTransform canvasTransform;

    float timeUntilReappear = 0;

    // Update is called once per frame
    void Update()
    {
        if(timeUntilReappear > 0)
        { timeUntilReappear -= Time.deltaTime; }
        else 
        { 
            makeImageReappear();
            keepVisualsOnMouse();
        }
    }

    public void makeImageDisappear()
    {
        handPointerImage.SetActive(false);
        timeUntilReappear = .1f;
    }

    void makeImageReappear()
    { handPointerImage.SetActive(true); }

    void keepVisualsOnMouse()
    {
        Vector2 pixelMousePos = Mouse.current.position.ReadValue();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTransform,
            pixelMousePos,
            null,
            out Vector2 mousePos
            );

        myTransform.localPosition = mousePos;
    }
}
