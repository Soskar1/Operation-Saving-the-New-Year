using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float maxTimeBtwSpawning;
    private float timer;

    [Header("Presents")]
    [SerializeField] private GameObject coockiePresent;
    [SerializeField] private GameObject snowballPresent;

    [Header("Spawnpoint Range")]
    [SerializeField] private Transform beginning;
    [SerializeField] private Transform end;

    [HideInInspector] public bool canSpawn;

    private void Update()
    {
        if (canSpawn)
        {
            if (timer < 0)
            {
                float xPosSpawnPoint = Random.Range(beginning.position.x, end.position.x);
                Vector2 spawnPoint = new Vector2(xPosSpawnPoint, beginning.position.y);

                int whichPresentToSpawn = Random.Range(1, 3);

                if (whichPresentToSpawn == 1)
                {
                    Instantiate(coockiePresent, spawnPoint, Quaternion.identity);
                }
                else
                {
                    Instantiate(snowballPresent, spawnPoint, Quaternion.identity);
                }

                timer = maxTimeBtwSpawning;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }
}
