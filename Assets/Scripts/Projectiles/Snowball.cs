using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : Bullet
{
    [SerializeField] private Rigidbody2D rb2d;

    void Update()
    {
        float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
