using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAndResumeButton : MonoBehaviour
{

    public GameObject pauseScreen;
    public GameObject topBar;

    public void PauseGame()
    {
        Time.timeScale = 0;
        topBar.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        topBar.SetActive(true);
    }
}
