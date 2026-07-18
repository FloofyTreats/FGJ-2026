using UnityEngine;

public class HandPinchController : MonoBehaviour
{
    [SerializeField] RectTransform myTransform;
    [SerializeField] RectTransform frequencyDialImgRectTransform;
    [SerializeField] RectTransform volumeDialImgRectTransform;
    

    float timeUntilDisappear = 0;

    // Update is called once per frame
    void Update()
    {
        if(timeUntilDisappear > 0)
        { timeUntilDisappear -= Time.deltaTime; }    
        else
        {
            myTransform.parent = null;
            gameObject.SetActive(false); 
        }    
    }

    public void turnOn()
    {
        timeUntilDisappear = .1f;
        gameObject.SetActive(true);
    }

    public void turnOnForFrequency()
    {
        turnOn();
        myTransform.parent = frequencyDialImgRectTransform;
        myTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void turnOnForVolume()
    {
        turnOn();
        myTransform.parent = volumeDialImgRectTransform;
        myTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void updateRotation()
    {
       //myTransform.localEulerAngles = new Vector3(0,0,)
    }
}
