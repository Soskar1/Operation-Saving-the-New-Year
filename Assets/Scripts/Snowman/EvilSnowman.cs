using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSnowman : MonoBehaviour
{
    public float speed;
    [SerializeField] private bool facingRight;
    [SerializeField] private float sightDistance;

    [Header("Snowman's Components")]
    public GameObject arms;
    [SerializeField] private Health health;
    [SerializeField] private BoxCollider2D snowmanCollider;
    //[SerializeField] private BoxCollider2D deathCollider;

    [SerializeField] private bool mage;

    [Header("Target")]
    [HideInInspector] public bool seeTarget;
    private Transform target;

    [Header("Animation")]
    public Animator animator;

    private void Awake()
    {
        //отключаем коллизию между врагом и вражескими снарядами
        Physics2D.IgnoreLayerCollision(10, 12);

        //между врагом и игроком
        Physics2D.IgnoreLayerCollision(9, 10);
        target = FindObjectOfType<Movement>().GetComponent<Transform>();

        //между врагом и боссом
        Physics2D.IgnoreLayerCollision(10, 14);
    }

    private void Update()
    {
        if (seeTarget)
        {
            if (target != null)
            {
                //Всегда глядим на нашу цель
                if (target.position.x < transform.position.x && facingRight ||
                    target.position.x > transform.position.x && !facingRight)
                {
                    Flip();
                }

                //Если цель убежала дальше дальности видимости, то начинаем преследование
                if (Vector2.Distance(transform.position, target.position) > sightDistance)
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime);

                    animator.SetBool("IsRunning", true);
                }
                else
                {
                    animator.SetBool("IsRunning", false);
                }
            }
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);
    }

    public void Smelting()
    {
        animator.SetBool("IsSmelting", true);

        if (arms != null)
        {
            arms.SetActive(false);
        }

        if (mage)
        {
            GetComponentInChildren<Mage>().RevertSettings();
        }

        Destroy(health);
        seeTarget = false;
        target = null;

        snowmanCollider.enabled = false;
        //deathCollider.enabled = true;
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}