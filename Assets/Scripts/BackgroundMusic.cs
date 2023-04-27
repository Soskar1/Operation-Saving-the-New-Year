using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance = null;
    [HideInInspector] public AudioSource source;
    public AudioClip xmasMusic;
    public AudioClip xmasBossMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }

    public void ChangeMusic(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
