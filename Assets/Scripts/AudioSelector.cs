using Microsoft.Extensions.Logging.Abstractions;
using UnityEngine;

public class AudioSelector : MonoBehaviour
{
    AudioSelection[] audioSelectionArray;

    private void Start()
    {
        audioSelectionArray = GetComponentsInChildren<AudioSelection>();
    }

    public AudioSelection tryToGetNewAudioSource(float frequency)
    {
        AudioSelection returnAudio = null;

        foreach (AudioSelection audioSelection in audioSelectionArray) 
        {

            if (audioSelection.minFrequencyRange < frequency && audioSelection.maxFrequencyRange > frequency)
            {
                returnAudio = audioSelection;
                audioSelection.radioAudioSource.Play();
            }
            else
            { 
                audioSelection.radioAudioSource.Pause();
            }
        }
        
        return returnAudio; 
    }

    public void pauseAllTracks()
    {
        foreach (AudioSelection audioSelection in audioSelectionArray)
        {
            if(audioSelection.radioAudioSource.isPlaying == true)
            {
                audioSelection.radioAudioSource.Pause();
            }
        }
    }
}
