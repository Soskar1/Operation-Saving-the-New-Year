using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private void Awake()
    {
        //Снаряды и триггеры
        Physics2D.IgnoreLayerCollision(11, 13);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<EvilSnowman>() != null)
        {
            if (collision.GetComponentInChildren<MiniSnowman>() != null)
            {
                if (collision.GetComponentInChildren<MiniSnowman>().isGrounded)
                {
                    collision.GetComponentInParent<EvilSnowman>().Smelting();
                }
            }
            else
            {
                collision.GetComponentInParent<EvilSnowman>().Smelting();
            }
        }
    }
}
