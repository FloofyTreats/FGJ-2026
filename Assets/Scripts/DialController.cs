using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialController : MonoBehaviour
{
    [SerializeField] RectTransform parentCanvasRect;
    [SerializeField] RectTransform buttonTrans;
    [SerializeField] Camera canvasCamera;
    [SerializeField] UnityEvent clickingInsideArea;
    [SerializeField] RectTransform imageToRotate;

    Vector2 buttonPos;
    float angle = 0; public float Angle {  get { return angle; }}

    public void updateDial()
    {
        buttonPos = buttonTrans.anchoredPosition;

        bool badAngle = checkNoGoWest45Degrees();
        if(badAngle == true)
        {
            angle = 0;
            return;
        }

        calculateAngle();
        updateImage();
        clickingInsideArea.Invoke();
    }

    bool checkNoGoWest45Degrees()
    {
        Vector2 westVec = new Vector2(-1f, 0);


        Vector2 pixelMousePos = Mouse.current.position.ReadValue();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            buttonTrans,
            pixelMousePos,
            null,
            out Vector2 mousePos
            );

        float angle = Vector2.Angle(westVec, mousePos.normalized);

        if(angle < 45)
        { return true; }

        return false;
    }

    void calculateAngle()
    {
        Vector2 westVec = new Vector2(-1f, 0);

        Vector2 pixelMousePos = Mouse.current.position.ReadValue();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            buttonTrans,
            pixelMousePos,
            null,
            out Vector2 mousePos
            );

        float holdAngle = Vector2.Angle(westVec, mousePos.normalized);

        if(mousePos.y > 0)
        {
            angle = holdAngle - 45;
        }
        else
        {
            angle = 135;
            angle += (180 - holdAngle);

        }
        //angle += holdAngle;
    }

    void updateImage()
    {
        imageToRotate.localEulerAngles = new Vector3(0, 0, angle * -1);
    }
}
