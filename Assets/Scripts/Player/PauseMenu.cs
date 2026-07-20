using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public FirstPersonController controller;
    public GameObject pauseCanvas;
    public GameObject quitCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = FindAnyObjectByType<FirstPersonController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetButtonDown("Cancel") && !controller.inUI) {
            Pause();
        }
    }

    public void Pause()
    {
        controller.ToggleInUI();
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        controller.ToggleInUI();
    }

    public void Quit()
    {
        quitCanvas.SetActive(true);
    }

    public void Cancel()
    {
        quitCanvas.SetActive(false);
    }

    public void QuitConfirm()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
