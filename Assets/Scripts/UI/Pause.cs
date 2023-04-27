using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject settingsWindow;
    private Game game;

    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    public void Resume()
    {
        GetComponent<Pause>().gameObject.SetActive(false);
        Time.timeScale = 1;
        game.inPause = false;

        if (settingsWindow.activeSelf)
        {
            settingsWindow.SetActive(false);
        }
    }

    public void Settings()
    {
        settingsWindow.SetActive(true);
    }

    public void MainMenu()
    {
        PlayerPrefs.DeleteAll();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        PlayerPrefs.DeleteAll();
        Time.timeScale = 1;
        Application.Quit();
    }

    public void Close()
    {
        settingsWindow.SetActive(false);
    }
}
