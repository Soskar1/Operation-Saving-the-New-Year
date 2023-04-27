using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Fire fire;

    public void Shoot()
    {
        fire.Shoot();
    }
}
