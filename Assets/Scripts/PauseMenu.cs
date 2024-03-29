using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    public GameObject pauseMenuUI;
    public GameObject optionsUI;
    public GameObject menuUI;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        player.GetComponent<Player>().enabled= true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        player.GetComponent<Player>().enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Menu pressed");
        menuUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void BackToMain()
    {
        menuUI.SetActive(true);
        optionsUI.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
    }
}