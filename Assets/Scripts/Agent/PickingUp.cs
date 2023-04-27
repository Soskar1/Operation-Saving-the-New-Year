using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUp : MonoBehaviour
{
    [SerializeField] private Equipping equipping;
    [SerializeField] private Shooting shooting;
    [SerializeField] private int ammoAmount;
    [HideInInspector] public bool canPickUp;

    [Header("Sounds")]
    [SerializeField] private AudioSource source;

    private GrabSnow currentLumpOfSnow;

    private void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp(ammoAmount);
                currentLumpOfSnow.NextLifeCycle();
            }
        }
    }

    void PickUp(int ammoAmount)
    {
        if (shooting.currentAmmo == shooting.maxAmmo)
        {
            return;
        }

        shooting.GetAmmo(ammoAmount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Looting>() != null)
        {
            Looting loot = collision.GetComponent<Looting>();
            if (!loot.flamethrower)
            {
                source.Play();
                PickUp(loot.amountOfSnowballs);
            }
            else
            {
                source.Play();
                equipping.canEquipFlamethrower = true;
            }

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<GrabSnow>() != null)
        {
            currentLumpOfSnow = collision.GetComponent<GrabSnow>();
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<GrabSnow>() != null)
        {
            currentLumpOfSnow = null;
            canPickUp = false;
        }
    }
}
