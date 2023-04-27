using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private BlackScreen blackScreen;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float maxHealth;
    [HideInInspector] public float currentHealth;

    [Header("For Boss")]
    [SerializeField] private Krampus krampus;
    [SerializeField] private bool needToMoveToTheNextPhase;

    [Header("Sounds")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip healClip;
    private BackgroundMusic music;

    private void Awake()
    {
        music = FindObjectOfType<BackgroundMusic>();

        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;

        source.clip = hitClip;
        source.Play();

        if (GetComponentInChildren<Detection>() != null)
        {
            GetComponentInChildren<Detection>().InformOthers();
            GetComponentInChildren<Detection>().DetectTarget();
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (needToMoveToTheNextPhase)
        {
            if (currentHealth <= maxHealth / 2)
            {
                krampus.MoveToSecondPhase();
                needToMoveToTheNextPhase = false;
            }
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        source.clip = healClip;
        source.Play();

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    private void Death()
    {
        if (GetComponent<Drop>() != null)
        {
            GetComponent<Drop>().DropLoot();
        }

        if (GetComponentInParent<Movement>() != null)
        {
            //Перезагрузка сцены
            StartCoroutine(blackScreen.LoadScene(SceneManager.GetActiveScene().buildIndex));

            //Отключение движения перса + октлючение его визуала
            GetComponentInParent<Movement>().Disappear();

            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                music.ChangeMusic(music.xmasMusic);
            }
        }
        else
        {
            if (krampus != null)
            {
                krampus.KillAllSnowmans();
            }

            Destroy(gameObject);
        }
    }
}
