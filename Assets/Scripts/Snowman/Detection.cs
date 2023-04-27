using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [Header("Snowman")]
    [SerializeField] private EvilSnowman snowman;
    [SerializeField] private Mage mage;
    public GameObject arms;

    [Header("Informing")]
    [SerializeField] private float informRadius;
    [SerializeField] private LayerMask whomToInform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Movement>() != null)
        {
            InformOthers();
            DetectTarget();
        }
    }

    public void DetectTarget()
    {
        snowman.seeTarget = true;
        snowman.animator.SetBool("See Target", true);

        if (arms != null)
        {
            arms.SetActive(true);
        }

        if (mage != null)
        {
            mage.MageInTheAir();
        }

        Physics2D.IgnoreLayerCollision(9, 10);
        Destroy(gameObject);
    }

    public void InformOthers()
    {
        Collider2D[] allies = Physics2D.OverlapCircleAll(transform.position, informRadius, whomToInform);

        if (allies != null)
        {
            foreach (Collider2D allie in allies)
            {
                //allie.GetComponentInParent<EvilSnowman>().seeTarget = true;
                //allie.GetComponentInParent<EvilSnowman>().animator.SetBool("See Target", true);
                //allie.GetComponentInParent<EvilSnowman>().arms.SetActive(true);
                if (allie.GetComponentInChildren<Detection>() != null)
                {
                    allie.GetComponentInChildren<Detection>().DetectTarget();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        float theta = 0;
        float x = informRadius * Mathf.Cos(theta);
        float y = informRadius * Mathf.Sin(theta);
        Vector3 pos = transform.position + new Vector3(x, y, 0);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = informRadius * Mathf.Cos(theta);
            y = informRadius * Mathf.Sin(theta);
            newPos = transform.position + new Vector3(x, y, 0);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
        Gizmos.DrawLine(pos, lastPos);
    }
}
