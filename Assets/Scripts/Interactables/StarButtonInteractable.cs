using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StarButtonInteractable : Interactable
{
    public RadioFrequencyController radioFrequencyController;
    public SoundManipulationController soundManipulationController;
    public string validFrequency;
    public NormalDoorInteractable door;
    
    private bool unlocked = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public override void Interact()
    {
        if (!audioSource.isPlaying) { 
            audioSource.Play();
        }

        if(unlocked)
        {
            return;
        }

        if (radioFrequencyController.Frequency.ToString("F1") == validFrequency && soundManipulationController.PlaybackSpeed < 0) {
            UnlockDoor();
        }
    }

    public void UnlockDoor()
    {
        unlocked = true;
        Level1Manager.Instance.EndDemo();
        door.unlocked = true;
    }
}
