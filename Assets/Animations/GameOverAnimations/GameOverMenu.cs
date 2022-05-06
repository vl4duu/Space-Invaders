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
        retryButton.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        backButton.SetActive(true);
    }

    public void OnRetry()
    {
        
        SceneManager.LoadScene("Game");

    }

    public void OnBack()
    {
        
        SceneManager.LoadScene("Menu");
    }



}
