using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SoundManipulationController : MonoBehaviour
{
    AudioSource currentAudioSource;
    AudioMixer currentAudioMixer;

    [SerializeField] AudioSource radioAudioSource;
    [SerializeField] AudioSource tuningAudioSource;
    [SerializeField] AudioMixer radioAudioMixer;
    [SerializeField] UnityEvent radioPowered;


    //[SerializeField] AudioSource zelda2TrackAudioSource;
    //[SerializeField] AudioMixer zelda2TrackAudioMixer;

    [SerializeField] AudioSelector audioSelector;

    [SerializeField] TextMeshProUGUI PlaybackRateTextbox;

    float playbackSpeed = 1f; public float PlaybackSpeed { get { return playbackSpeed; } }
    float volume = .5f;
    bool tuning = false;
    public bool powered = false;

    enum TypeOfSound 
    { 
        staticNoise,
        radioStation
    }

    TypeOfSound typeOfSound = TypeOfSound.staticNoise;

    private void Awake()
    {
        radioAudioSource.Stop();
        updatePlaybackSpeedUIText();
    }

    void Update()
    {
        if (Input.GetButtonDown("Debug1"))
        {
            Debug.Log("toggling power");
            TogglePower();
        }
        checkIfResetReversedAudioLoop();
    }

    public void setRadioStatic()
    {
        if (!powered)
        {
            return;
        }
        currentAudioSource = radioAudioSource;
        currentAudioMixer = radioAudioMixer;
        typeOfSound = TypeOfSound.staticNoise;
        if (radioAudioSource.isPlaying == false)
        {
            radioAudioSource.UnPause();

            if(radioAudioSource.isPlaying == false)
            {
                radioAudioSource.Play();
            }
        }
        audioSelector.pauseAllTracks();

        refreshPlayback();
    }

    public void setNewFrequency(float frequency)
    {
        if(!powered)
        {
            return;
        }

        AudioSelection audioSelection = audioSelector.tryToGetNewAudioSource(frequency);

        if (audioSelection == null)
        {
            setRadioStatic();
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
        if (!powered)
        {
            return;
        }
        if (playbackSpeed >= 0f)
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
        if (!powered)
        {
            return;
        }
        if (playbackSpeed >= 0)
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
        if(!powered)
        {
            return;
        }
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
        if(!powered)
        {
            return;
        }

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
        if(!powered)
        {
            return;
        }

        tuningAudioSource.Stop();
        tuning = false;

        switch(typeOfSound)
        {
            case TypeOfSound.radioStation:
                {
                    currentAudioSource.pitch = playbackSpeed;
                    currentAudioSource.volume = volume;
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

    void StopPlayback()
    {
        currentAudioSource.Stop();
    }

    public void TuneRadio()
    {
        if (tuning || !powered)
        {
            return;
        }

        tuning = true;

        currentAudioSource.Pause();
        tuningAudioSource.Play();
    }
    
    public void TogglePower()
    {
        powered = !powered;

        if(!powered)
        {
            StopPlayback();
        }

        radioPowered.Invoke();
    }
}
