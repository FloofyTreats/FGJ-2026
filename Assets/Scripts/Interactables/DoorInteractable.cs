using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorInteractable : Interactable
{
    public string nextLevel;
    public bool locked = false;

    private FirstPersonController controller;
    private ScreenFade screenFade;

    private void Start()
    {
        controller = FindAnyObjectByType<FirstPersonController>();
        screenFade = FindAnyObjectByType<ScreenFade>();
    }
    public override void Interact()
    {
        if( locked )
        {
            StartCoroutine(controller.displayCaption("It's locked.", 3f));
        }
        else
        {
            StartCoroutine("ChangeLevel");
        }
    }

    public void ToggleDoorLock(bool val)
    {
        locked = val;
    }

    IEnumerator ChangeLevel()
    {
        controller.LockMovement();
        screenFade.FadeOut();

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(nextLevel);
    }
}
