using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Krampus : MonoBehaviour
{
    [HideInInspector] public bool canAttack;
    private Transform target;
    private Game game;

    [Header("Timer")]
    [SerializeField] private float maxTimeBtwAbilities;
    private float timer;

    [Header("First Phase")]
    private bool firstPhase = true;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private BoxCollider2D firstPhaseHitbox;

    [Header("First Ability")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float damage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private LayerMask whoIsEnemy;

    [Header("Second Ability")]
    [SerializeField] private GameObject snowman;
    [SerializeField] private Transform firstSpawnPoint;
    [SerializeField] private Transform secondSpawnPoint;

    [Header("Second Phase")]
    [SerializeField] private float speed;
    [SerializeField] private BoxCollider2D secondPhaseHitbox;
    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;
    private bool secondPhase;
    private bool moveRight;

    [Header("Third Ability")]
    [SerializeField] private GameObject lavaArrow;
    [SerializeField] private Transform shotPos;

    [Header("Fourth Ability")]
    [SerializeField] private GameObject snowmanMage;
    [SerializeField] private Transform secondPhase_firstSpawnPoint;
    [SerializeField] private Transform secondPhase_secondSpawnPoint;
    [SerializeField] private Transform secondPhase_thirdSpawnPoint;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Other")]
    [SerializeField] private bool facingRight;

    private void Awake()
    {
        game = FindObjectOfType<Game>();

        timer = maxTimeBtwAbilities;
        target = FindObjectOfType<Movement>().gameObject.transform;

        //между игроком и боссом
        Physics2D.IgnoreLayerCollision(9, 14);
    }

    public void Update()
    {
        if (canAttack)
        {
            if (timer < 0)
            {
                int whichAbilityToPlay = Random.Range(1, 3);

                if (firstPhase)
                {
                    if (whichAbilityToPlay == 1)
                    {
                        FirstAbility();
                    }
                    else if (whichAbilityToPlay == 2)
                    {
                        SecondAbility();
                    }
                }
                else if (secondPhase)
                {
                    if (whichAbilityToPlay == 1)
                    {
                        ThirdAbility();
                    }
                    else if (whichAbilityToPlay == 2)
                    {
                        FourthAbility();
                    }
                }

                timer = maxTimeBtwAbilities;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        //Смотрим на цель
        if (target.position.x < transform.position.x && facingRight ||
            target.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }

        if (secondPhase)
        {
            if (transform.position.x < leftBorder.position.x)
            {
                moveRight = true;
            }

            if (transform.position.x > rightBorder.position.x)
            {
                moveRight = false;
            }
        }    
    }

    private void FixedUpdate()
    {
        if (secondPhase)
        {
            if (moveRight)
            {
                rb2d.AddForce(Vector2.right * speed);
            }
            else
            {
                rb2d.AddForce(Vector2.left * speed);
            }
        }
    }

    public void FirstAbility()
    {
        animator.SetTrigger("First Ability");

        float distanceFromTarget = Vector2.Distance(transform.position, target.position);

        if (!facingRight)
        {
            rb2d.AddForce(Vector2.left * jumpForce * distanceFromTarget / 10);
        }
        else
        {
            rb2d.AddForce(Vector2.right * jumpForce * distanceFromTarget / 10);
        }
    }

    public void SecondAbility()
    {
        animator.SetTrigger("Second Ability");
    }

    public void MeleeAttack()
    {
        Collider2D[] objects = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0, whoIsEnemy);

        foreach(Collider2D player in objects)
        {
            if (player.gameObject.GetComponent<Movement>() != null)
            {
                player.gameObject.GetComponent<Health>().Hit(damage);
            }
        }
    }

    public void SpawnSnowmans()
    {
        GameObject firstSnowman = Instantiate(snowman, firstSpawnPoint.position, Quaternion.identity);
        GameObject secondSnowman = Instantiate(snowman, secondSpawnPoint.position, Quaternion.identity);

        firstSnowman.GetComponentInChildren<Detection>().DetectTarget();
        secondSnowman.GetComponentInChildren<Detection>().DetectTarget();
    }

    public void MoveToSecondPhase()
    {
        canAttack = false;
        rb2d.gravityScale = 0;
        firstPhaseHitbox.enabled = false;
        secondPhaseHitbox.enabled = true;

        animator.SetTrigger("Second Phase");
        firstPhase = false;
    }

    public void ChangeSettings()
    {
        canAttack = true;
        secondPhase = true;

        maxTimeBtwAbilities = 3.5f;
    }

    public void ThirdAbility()
    {
        animator.SetTrigger("Third Ability");
    }

    public void InstantiateLavaArrow()
    {
        Instantiate(lavaArrow, shotPos.position, shotPos.rotation);
    }

    public void FourthAbility()
    {
        animator.SetTrigger("Fourth Ability");
    }

    public void SecondPhaseSpawning()
    {
        GameObject firstSnowman = Instantiate(snowman, secondPhase_firstSpawnPoint.position, Quaternion.identity);
        GameObject secondSnowman = Instantiate(snowman, secondPhase_secondSpawnPoint.position, Quaternion.identity);
        GameObject thirdSnowman = Instantiate(snowmanMage, secondPhase_thirdSpawnPoint.position, Quaternion.identity);

        firstSnowman.GetComponentInChildren<Detection>().DetectTarget();
        secondSnowman.GetComponentInChildren<Detection>().DetectTarget();
        thirdSnowman.GetComponentInChildren<Detection>().DetectTarget();
    }

    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);
    }

    public void KillAllSnowmans()
    {
        EvilSnowman[] snowmans = FindObjectsOfType<EvilSnowman>();

        foreach (EvilSnowman snowman in snowmans)
        {
            Destroy(snowman.gameObject);
        }

        game.FinishTheGame();
    }

    private void OnDrawGizmosSelected()
    {
        //площадь, по которой будет проходить урон от ближней атаки
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(attackPoint.position.x - attackSize.x / 2, attackPoint.position.y + attackSize.y / 2),
            new Vector2(attackPoint.position.x + attackSize.x / 2, attackPoint.position.y + attackSize.y / 2));

        Gizmos.DrawLine(new Vector2(attackPoint.position.x - attackSize.x / 2, attackPoint.position.y + attackSize.y / 2),
            new Vector2(attackPoint.position.x - attackSize.x / 2, attackPoint.position.y - attackSize.y / 2));

        Gizmos.DrawLine(new Vector2(attackPoint.position.x - attackSize.x / 2, attackPoint.position.y - attackSize.y / 2),
            new Vector2(attackPoint.position.x + attackSize.x / 2, attackPoint.position.y - attackSize.y / 2));

        Gizmos.DrawLine(new Vector2(attackPoint.position.x + attackSize.x / 2, attackPoint.position.y - attackSize.y / 2),
            new Vector2(attackPoint.position.x + attackSize.x / 2, attackPoint.position.y + attackSize.y / 2));
    }
}
