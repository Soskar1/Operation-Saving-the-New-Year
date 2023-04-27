using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Shooting
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Collider2D dangerZone;

    [SerializeField] private AudioSource flameSource;

    private void Update()
    {
        if (!game.inPause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                flameSource.Play();
                particles.Play();
                dangerZone.enabled = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                flameSource.Stop();
                particles.Stop();
                dangerZone.enabled = false;
            }
        }
    }
}
