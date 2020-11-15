using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Debug.Log("Went back to main menu");
        //Application.Quit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
