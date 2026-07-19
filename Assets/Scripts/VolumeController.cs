using UnityEngine;
using UnityEngine.Events;

public class VolumeController : MonoBehaviour
{
    [SerializeField] SoundManipulationController soundManipulationController;
    [SerializeField] DialController dialController;
    [SerializeField] UnityEvent volumeSet;
    float volume; public float Volume { get { return volume; } }

    private void Start()
    {
        volume = 0.5f;
    }

    public void updateBasedOnDialAngle()
    {

        float angle = dialController.Angle;

        //float volume = .25f + (.5f * (angle / 270));
        volume = 0 + (1f * (angle / 270));

        soundManipulationController.setVolume(volume);
        volumeSet.Invoke();
    }
}
