using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSnowman : MonoBehaviour
{
    [SerializeField] private EvilSnowman evilSnowman;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject hand;

    [Header("Other")]
    [SerializeField] private float speedAfterAppearing;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [HideInInspector] public bool isGrounded;

    private void Awake()
    {
        //игрок и враг
        Physics2D.IgnoreLayerCollision(9, 10);
    }

    private void Update()
    {
        //Во время падения
        if (animator.GetBool("Grounded") == false)
        {
            //враг и враг
            Physics2D.IgnoreLayerCollision(10, 10, true);

            CheckForGround();
        }
    }

    private void CheckForGround()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, whatIsGround);

        if (hitInfo)
        {
            isGrounded = true;
            ActivatingBehaviour();
        }
    }

    private void ActivatingBehaviour()
    {
        animator.SetBool("Grounded", true);
        evilSnowman.speed = speedAfterAppearing;
        evilSnowman.seeTarget = true;
        Physics2D.IgnoreLayerCollision(10, 10, false);
    }

    public void ActivateHand()
    {
        hand.SetActive(true);
    }
}
