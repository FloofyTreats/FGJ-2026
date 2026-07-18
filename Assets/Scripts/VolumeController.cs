using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] SoundManipulationController soundManipulationController;
    [SerializeField] DialController dialController;

    public void updateBasedOnDialAngle()
    {

        float angle = dialController.Angle;

        //float volume = .25f + (.5f * (angle / 270));
        float volume = 0 + (1f * (angle / 270));

        soundManipulationController.setVolume(volume);
    }
}
