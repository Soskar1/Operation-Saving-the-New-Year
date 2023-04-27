using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EvilSnowman evilSnowman;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Collider2D originCollider;
    [SerializeField] private Collider2D newCollider;
    [SerializeField] private Animator animator;

    [Header("Abilities")]
    [SerializeField] private float maxTimeBtwAbilities;
    private float time;

    [Header("1st Ability")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject miniSnowman;

    [Header("2nd Ability")]
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject frostBolt;

    private void Awake()
    {
        time = maxTimeBtwAbilities;
    }

    private void Update()
    {
        if (evilSnowman.seeTarget)
        {
            if (time < 0)
            {
                int ability = Random.Range(1, 3);

                if (ability == 1)
                {
                    animator.SetTrigger("First Ability");
                }
                else if (ability == 2)
                {
                    animator.SetTrigger("Second Ability");
                }

                time = maxTimeBtwAbilities;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }

    public void MageInTheAir()
    {
        rb2d.gravityScale = 0;
        originCollider.enabled = false;
        newCollider.enabled = true;
    }

    public void FirstAbility()
    {
        Instantiate(miniSnowman, spawnPoint.position, Quaternion.identity);
    }

    public void SecondAbility()
    {
        Instantiate(frostBolt, shotPoint.position, shotPoint.rotation);
    }

    public void RevertSettings()
    {
        newCollider.enabled = false;
        rb2d.gravityScale = 2;
        GetComponent<Mage>().enabled = false;
    }
}
