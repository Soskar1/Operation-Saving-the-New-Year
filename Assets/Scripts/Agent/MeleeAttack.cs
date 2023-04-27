using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Movement movement;
    [SerializeField] private Equipping equipping;
    [SerializeField] private Animator animator;

    [Header("Bat Settings")]
    public float reflectionForce;
    [SerializeField] private float damage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private LayerMask whoIsEnemy;

    [Header("Sounds")]
    [SerializeField] private AudioSource source;

    [HideInInspector] public bool canReflect;

    private void Update()
    {
        if (!equipping.withWeapon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
            }
        }

        if (canReflect)
        {
            Collider2D[] objects = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0, whoIsEnemy);

            foreach (Collider2D enemy in objects)
            {
                if (enemy.gameObject.GetComponent<Snowball>() != null)
                {
                    enemy.gameObject.GetComponent<Snowball>().canBeReflected = true;
                }
            }
        }
    }

    public void Attack()
    {
        source.Play();
        canReflect = true;
        Collider2D[] objects = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0, whoIsEnemy);

        foreach(Collider2D enemy in objects)
        {
            if (enemy.gameObject.GetComponent<Health>() != null)
            {
                if (enemy.gameObject.GetComponent<Movement>() == null)
                {
                    enemy.gameObject.GetComponent<Health>().Hit(damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
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
