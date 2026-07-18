using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RadioFrequencyController : MonoBehaviour
{
    [SerializeField] FrequencySliderUIController slider;
    [SerializeField] SoundManipulationController soundManipulation;
    [SerializeField] DialController dialController;
    [SerializeField] Text FrequencyUITextbox;
    [SerializeField] SoundManipulationController soundManipulationController;
    float frequency; public float Frequency { get { return frequency; } }
    float timeUntilPullStation = 2f;
    bool triedToPullStation = true;
    void resetTimeUntilPullStation() 
    { 
        timeUntilPullStation = 2f;
        triedToPullStation = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frequency = 90.0f;
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
    
    void tryToPullStation()
    {
        triedToPullStation = true;

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
        soundManipulationController.setRadioStatic();

        resetTimeUntilPullStation();
    }

    void updateFrequencyUIText()
    {
        FrequencyUITextbox.text = frequency.ToString("F1");
    }
}
