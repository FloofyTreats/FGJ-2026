using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Events;

public class RadioFrequencyController : MonoBehaviour
{
    [SerializeField] FrequencySliderUIController slider;
    [SerializeField] SoundManipulationController soundManipulation;
    [SerializeField] DialController dialController;
    [SerializeField] TextMeshProUGUI FrequencyUITextbox;
    [SerializeField] SoundManipulationController soundManipulationController;
    [SerializeField] UnityEvent frequencySet;
    float frequency; public float Frequency { get { return frequency; } }
    float timeUntilPullStation = 2f;
    bool triedToPullStation = true;
    void resetTimeUntilPullStation() 
    { 
        timeUntilPullStation = 2f;
        triedToPullStation = false;
        soundManipulationController.TuneRadio();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frequency = 95.0f;
        updateFrequencyUIText();
        resetTimeUntilPullStation();
    }

    // Update is called once per frame
    void Update()
    {
        if(triedToPullStation == false)
        {
            if(timeUntilPullStation > 0)
            { timeUntilPullStation -= Time.deltaTime; }
            else
            {tryToPullStation();}
        }
    }
    
    public void tryToPullStation()
    {
        triedToPullStation = true;

        frequencySet.Invoke();

        soundManipulationController.setNewFrequency(frequency);
    }

    public void resetFrequency()
    {
        //slider.value = .333f;
        soundManipulation.makeSureNotReversed();
    }

    public void updateBasedOnDialAngle()
    {

        float angle = dialController.Angle;

        frequency = 90 + (10 * (angle / 270));
        slider.updateSliderUIBasedOnFrequency(frequency);
        updateFrequencyUIText();
        //soundManipulationController.setRadioStatic();

        resetTimeUntilPullStation();
    }

    void updateFrequencyUIText()
    {
        FrequencyUITextbox.text = frequency.ToString("F1");
    }
}
