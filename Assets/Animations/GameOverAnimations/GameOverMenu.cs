using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject retryButton;
    public GameObject backButton;


    public IEnumerator MakeButtonsAppear()
    {
        Debug.Log("Buttons appear");
        retryButton.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        backButton.SetActive(true);
    }

    public void OnRetry()
    {
        Debug.Log("Retry has been pressed");
        SceneManager.LoadScene("Game");

    }

    public void OnBack()
    {
        Debug.Log("BackHasBeenPressed");
        SceneManager.LoadScene("Menu");
    }



}
