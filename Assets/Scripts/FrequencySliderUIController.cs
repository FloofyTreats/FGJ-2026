using UnityEngine;
using UnityEngine.UI;

public class FrequencySliderUIController : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void updateSliderUIBasedOnFrequency(float frequency)
    { slider.value = frequency; }
}
