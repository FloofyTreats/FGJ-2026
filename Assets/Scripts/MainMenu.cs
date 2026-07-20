using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        screenFade.FadeIn();
    }
    public ScreenFade screenFade;
    public Canvas gallery;
    public void StartGame()
    {
        //SceneManager.LoadScene("Level 0");
        StartCoroutine("SwitchSceneCoroutine");
    }

    IEnumerator SwitchSceneCoroutine()
    {
        Debug.Log("why");
        screenFade.FadeOut();

        yield return new WaitForSeconds(3f);
        Debug.Log("after");

        SceneManager.LoadScene("Level 0");
    }
}
