using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private bool paused;
    public GameObject pauseMenu;

    void Start()
    {
        paused = false;
        pauseMenu.SetActive(false);
    }

    public void pauseBool()
    {
        paused = !paused;
    }

    public void exitGame()
    {
        Application.Quit();
    }
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            pauseBool();
        }

        if(paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
}
