using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SoundManipulationController : MonoBehaviour
{
    AudioSource currentAudioSource;
    AudioMixer currentAudioMixer;

    [SerializeField] AudioSource radioAudioSource;
    [SerializeField] AudioMixer radioAudioMixer;


    //[SerializeField] AudioSource zelda2TrackAudioSource;
    //[SerializeField] AudioMixer zelda2TrackAudioMixer;

    [SerializeField] AudioSelector audioSelector;

    [SerializeField] Text PlaybackRateTextbox;

    float playbackSpeed = 1f;
    float volume = .5f;

    enum TypeOfSound 
    { 
        staticNoise,
        radioStation
    }

    TypeOfSound typeOfSound = TypeOfSound.staticNoise;

    private void Awake()
    {
        updatePlaybackSpeedUIText();
        setRadioStatic();
    }

    void Update()
    {
        checkIfResetReversedAudioLoop();
    }

    public void setRadioStatic()
    {
        currentAudioSource = radioAudioSource;
        currentAudioMixer = radioAudioMixer;
        typeOfSound = TypeOfSound.staticNoise;
        if (radioAudioSource.isPlaying == false)
        { radioAudioSource.UnPause(); }
        audioSelector.pauseAllTracks();

        refreshPlayback();
    }

    public void setNewFrequency(float frequency)
    {
        AudioSelection audioSelection = audioSelector.tryToGetNewAudioSource(frequency);

        if (audioSelection == null)
        {
            //setRadioStatic();
        }
        else
        {
            typeOfSound = TypeOfSound.radioStation;
            currentAudioSource = audioSelection.radioAudioSource;
            currentAudioMixer = audioSelection.radioAudioMixer;
            radioAudioSource.Pause();
        }

        refreshPlayback();
    }    

    public void setVolume(float volumeVal)
    {
        volume = volumeVal;
        refreshPlayback();
    }

    public void increasePlaybackSpeed()
    {
        if(playbackSpeed >= 0f)
        //if (zelda2TrackAudioSource.pitch >= 0)
        {
            playbackSpeed += .1f;
            if (playbackSpeed > 2)
            { playbackSpeed = 2; }
        }
        else
        {
            playbackSpeed -= .1f;
            if (playbackSpeed < -2)
            { playbackSpeed = -2; }
        }
        refreshPlayback();
    }

    public void decreasePlaybackSpeed()
    {
        if(playbackSpeed >= 0)
        //if (zelda2TrackAudioSource.pitch >= 0)
        {
            playbackSpeed -= .1f;
            if (playbackSpeed < .5f)
            { playbackSpeed = .5f; }
        }
        else
        {
            playbackSpeed += .1f;
            if (playbackSpeed > -.5f)
            { playbackSpeed = -.5f; }
        }
        refreshPlayback();
    }

    public void reversePlayback()
    {
        playbackSpeed *= -1f;
        refreshPlayback();
    }

    public void makeSureNotReversed()
    { 
        if(playbackSpeed < 0)
        { reversePlayback(); }
    }

    public void setPlaybackSpeedFromSlider(float sliderValue)
    { 
        if (playbackSpeed >= 0)
        { playbackSpeed = .5f + (sliderValue * 1.5f); }
        else
        { { playbackSpeed = -.5f + (sliderValue * -1.5f); } }
        refreshPlayback();
    }

    void checkIfResetReversedAudioLoop()
    {
        if(playbackSpeed < 0)
        //if(zelda2TrackAudioSource.pitch < 0)
        {
            if(currentAudioSource.time <= .01f || currentAudioSource.isPlaying == false)
            {
                currentAudioSource.time = currentAudioSource.clip.length - .05f;
                currentAudioSource.Play();
            }
        }
    }

    void updatePlaybackSpeedUIText()
    {
        PlaybackRateTextbox.text = playbackSpeed.ToString("F1");
    }

    void refreshPlayback()
    {
        currentAudioSource.volume = volume;

        switch(typeOfSound)
        {
            case TypeOfSound.radioStation:
                {
                    currentAudioSource.pitch = playbackSpeed;
                    //zelda2TrackAudioSource.pitch = playbackSpeed;
                    float pitchCorrection = 1f / playbackSpeed;
                    //zelda2TrackAudioMixer.SetFloat("MixerPitch", pitchCorrection);
                    currentAudioMixer.SetFloat("MixerPitch", pitchCorrection);
                    updatePlaybackSpeedUIText();
                    break; 
                }
            case TypeOfSound.staticNoise:
                {
                    break;
                }
        }
    }

}
