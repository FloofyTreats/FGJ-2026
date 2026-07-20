using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public ScreenFade screenFade;
    public Canvas gallery;
    public void StartGame()
    {
        StartCoroutine("SwitchSceneCoroutine");
    }

    IEnumerator SwitchSceneCoroutine()
    {
        screenFade.FadeOut();

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Level 0");
    }

    public void ToggleGallery()
    {
        gallery.enabled = !gallery.enabled;
    }
}
