using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Aiming : MonoBehaviour
{
    private Transform target;

    private void Awake()
    {
        target = FindObjectOfType<Movement>().gameObject.transform;
    }

    void Update()
    {
        RotateTowards(target.position);
    }

    private void RotateTowards(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
