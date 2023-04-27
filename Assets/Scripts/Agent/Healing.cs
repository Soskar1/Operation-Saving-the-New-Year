using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] private float healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Movement>() != null)
        {
            collision.GetComponentInParent<Health>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
