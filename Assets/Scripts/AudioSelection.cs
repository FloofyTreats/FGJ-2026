using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSelection : MonoBehaviour
{
    [SerializeField] public float minFrequencyRange;
    [SerializeField] public float maxFrequencyRange;

    [SerializeField] public AudioSource radioAudioSource;
    [SerializeField] public AudioMixer radioAudioMixer;
}
