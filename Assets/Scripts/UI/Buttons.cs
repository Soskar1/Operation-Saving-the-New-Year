using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private GameObject currentWindow;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenWindow(GameObject window)
    {
        if (currentWindow != null)
        {
            CloseWindow(currentWindow);
        }

        window.SetActive(true);
        currentWindow = window;
    }

    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
        currentWindow = null;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
