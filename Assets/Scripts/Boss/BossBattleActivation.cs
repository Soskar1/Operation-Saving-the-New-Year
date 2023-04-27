using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossBattleActivation : MonoBehaviour
{
    [Header("Krampus")]
    [SerializeField] private Krampus krampus;
    [SerializeField] private GameObject krampusHealthBarObject;
    [SerializeField] private GameObject bulletShield;
    [SerializeField] private GameObject entranceShield;

    [Header("Bounds")]
    [SerializeField] private CinemachineConfiner confiner;
    [SerializeField] private CompositeCollider2D bounds;
    [SerializeField] private GameObject invisibleWall;

    [Header("Santa")]
    [SerializeField] private Santa santa;

    private BackgroundMusic backgroundMusic;

    private void Awake()
    {
        backgroundMusic = FindObjectOfType<BackgroundMusic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Movement>() != null)
        {
            if (entranceShield != null)
            {
                entranceShield.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Movement>() != null)
        {
            confiner.m_BoundingShape2D = bounds;
            invisibleWall.SetActive(true);

            krampus.canAttack = true;
            krampusHealthBarObject.SetActive(true);

            santa.canSpawn = true;

            if (bulletShield != null || entranceShield != null)
            {
                Destroy(bulletShield);
                Destroy(entranceShield);
            }

            //Между триггерами и врагами
            Physics2D.IgnoreLayerCollision(10, 13);

            //Враги и подарки
            Physics2D.IgnoreLayerCollision(10, 15);

            //Босс и подарки
            Physics2D.IgnoreLayerCollision(14, 15);

            if (backgroundMusic.source.clip != backgroundMusic.xmasBossMusic)
            {
                backgroundMusic.ChangeMusic(backgroundMusic.xmasBossMusic);
            }
        }
    }
}
