using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private ParticleSystem reflectionParticles;

    [SerializeField] private GameObject visual;
    [SerializeField] private Collider2D bulletCol;

    [SerializeField] private float speed;
    [SerializeField] private bool allowMovement;
    [HideInInspector] public bool canBeReflected;

    [Header("Sound")]
    [SerializeField] private AudioSource source;

    void Awake()
    {
        //Снаряды и триггеры
        Physics2D.IgnoreLayerCollision(11, 13);

        Invoke("LifeTime", lifeTime);
    }

    private void LifeTime()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (allowMovement)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Ground")
        {
            Destroying();
        }

        if (coll.GetComponentInParent<Health>() != null) //Пуля касается существа
        {
            if (coll.GetComponentInChildren<MeleeAttack>() != null)
            {
                if (coll.GetComponentInChildren<MeleeAttack>().canReflect && canBeReflected)
                {
                    Reflection(coll.GetComponentInChildren<MeleeAttack>().reflectionForce);
                }
                else
                {
                    coll.GetComponentInParent<Health>().Hit(damage);

                    Destroying();
                }
            }
            else
            {
                coll.GetComponentInParent<Health>().Hit(damage);

                Destroying();
            }
        }
    }

    void Reflection(float force)
    {
        source.Play();
        transform.Rotate(0, 0, 180);
        GetComponent<Rigidbody2D>().velocity = transform.right * force;

        if (reflectionParticles != null)
        {
            reflectionParticles.Play();
        }

        damage += 50;

        //Изменяем слой снаряда на "дружественный"
        gameObject.layer = 11;
    }

    void Destroying()
    {
        bulletCol.enabled = false;
        Destroy(visual);

        if (reflectionParticles != null)
        {
            reflectionParticles.Play();
        }

        Destroy(gameObject, 0.5f);
    }
}
