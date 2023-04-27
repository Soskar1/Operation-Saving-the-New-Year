using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Game game;

    [Header("Movement Settings")]
    [HideInInspector] public float speed;
    public float maxSpeed;
    [SerializeField] private bool facingRight;

    [Header("Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;

    [Header("References")]
    [SerializeField] private BlackScreen blackScreen;
    [SerializeField] private Transform visual;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    private float horizontalMove;

    private void Awake()
    {
        game = FindObjectOfType<Game>();

        speed = maxSpeed;

        //Отключаем коллизию между игроком и "дружественными" снарядами
        Physics2D.IgnoreLayerCollision(9, 11);
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            Jump();
        }

        //Поворот персонажа
        if (horizontalMove > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalMove < 0 && facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(horizontalMove * speed * Time.fixedDeltaTime, 0));
    }

    private void Jump()
    {
        //Прыжок
        rb2d.AddForce(Vector2.up * jumpForce);
    }

    private bool GroundCheck()
    {
        //Проверка на нахождении персонажа на земле
        Collider2D hitInfo = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);

        if (hitInfo != null)
        {
            return true;
        }

        return false;
    }

    void Flip()
    {
        //Отзеркаливаем спрайт перса
        facingRight = !facingRight;

        Vector3 theScale = visual.localScale;
        theScale.x *= -1;
        visual.localScale = theScale;
    }

    public void Disappear()
    {
        visual.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        if (GetComponentInChildren<Aiming>() != null)
        {
            GetComponentInChildren<Aiming>().gameObject.SetActive(false);
        }

        speed = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Final"))
        {
            game.SaveAllInfo();

            StartCoroutine(blackScreen.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
}
