using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CheckIfHoldingMouseClickOnImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] UnityEvent holdingMouseEvent;
    [SerializeField] UnityEvent clickMouseEvent;
    bool isHolding = false;

    // Update is called once per frame
    void Update()
    {
        if(isHolding == true)
        {
            onMouseHold();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left) 
        {
            clickMouseEvent.Invoke();
            isHolding = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isHolding = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    { isHolding = false; }
    int count = 0;
    void onMouseHold()
    {
        holdingMouseEvent.Invoke();
    }
}
