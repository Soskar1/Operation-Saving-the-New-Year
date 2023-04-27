using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipping : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] private Animator animator;

    [Header("Arms")]
    [SerializeField] private GameObject arms;

    [Header("Weapons")]
    public bool canEquipFlamethrower;
    [SerializeField] private List<GameObject> weapons;
    [HideInInspector] public bool withWeapon;

    void Update()
    {
        //Снежкострел
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                EquipWeapon(0);
            }
        }

        //Огнемёт
        if (Input.GetKeyDown(KeyCode.Alpha2) && canEquipFlamethrower)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                EquipWeapon(1);
            }
        }

        //Бита
        if (Input.GetKeyDown(KeyCode.F))
        {
            EquipBat();
        }
    }

    private void EquipWeapon(int weaponID)
    {
        for (int index = 0; index < weapons.Count; index++)
        {
            if (weapons[index].activeSelf && weapons[index].GetComponent<Shooting>().weaponID == weaponID)
            {
                break;
            }
            else
            {
                if (weapons[index].GetComponent<Shooting>().weaponID != weaponID)
                {
                    weapons[index].SetActive(false);
                }
                else
                {
                    weapons[index].SetActive(true);

                    ActivateArms();
                    animator.SetBool("With Weapon", true);
                    withWeapon = true;
                }
            }
        }
    }

    private void EquipBat()
    {
        for (int index = 0; index < weapons.Count; index++)
        {
            weapons[index].SetActive(false);
        }

        DeactivateArms();
        animator.SetBool("With Weapon", false);
        withWeapon = false;
    }

    public void ActivateArms()
    {
        if (!arms.activeSelf)
        {
            arms.SetActive(true);
        }
    }

    public void DeactivateArms()
    {
        if (arms.activeSelf)
        {
            arms.SetActive(false);
        }
    }
}