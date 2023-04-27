using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public AudioMixer audioMixer;
    private float volume;

    private void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("volume");
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);

        this.volume = slider.value;
        PlayerPrefs.SetFloat("volume", this.volume);
    }
}
