using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Settings")]
    public List<Transform> shotPos;
    public GameObject snowball;
    public float spread;
    [SerializeField] private float launchForce;

    [Header("Sounds")]
    [SerializeField] private AudioSource source;
    [SerializeField] private List<AudioClip> clips;

    public void Shoot()
    {
        if (shotPos.Count == 1)
        {
            GameObject snowballInstance = Instantiate(snowball, shotPos[0].position, transform.rotation);
            snowballInstance.transform.Rotate(0, 0, Random.Range(-spread, spread));
            snowballInstance.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
        }
        else
        {
            for (int index = 0; index < shotPos.Count; index++)
            {
                GameObject snowballInstance = Instantiate(snowball, shotPos[index].position, transform.rotation);
                snowballInstance.transform.Rotate(0, 0, Random.Range(-spread, spread));
                snowballInstance.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
            }
        }

        //int whichSoundToPlay = Random.Range(0, clips.Count);
        //source.clip = clips[whichSoundToPlay];
        //source.Play();
    }
}
