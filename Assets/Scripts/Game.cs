using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject finalPanel;
    [HideInInspector] public bool inPause;

    [Header("Saving")]
    [SerializeField] private Health health;
    [SerializeField] private Shooting shooting;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 ||
            SceneManager.GetActiveScene().buildIndex == 3)
        {
            LoadAllInfo();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                inPause = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                inPause = false;
            }
        }
    }

    public void SaveAllInfo()
    {
        PlayerPrefs.SetFloat("Health", health.currentHealth);
        PlayerPrefs.SetInt("Ammo", shooting.currentAmmo);
    }

    private void LoadAllInfo()
    {
        health.currentHealth = PlayerPrefs.GetFloat("Health");
        shooting.currentAmmo = PlayerPrefs.GetInt("Ammo");

        FindObjectOfType<HealthBar>().SetHealth(health.currentHealth);
        shooting.SetAmmoText();
    }

    public void FinishTheGame()
    {
        finalPanel.SetActive(true);
    }
}
