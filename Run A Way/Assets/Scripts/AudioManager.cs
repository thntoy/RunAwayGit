using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SFXSource;

    private float _SFXVolume;
    private float _BGMVolume;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1);
        _BGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1);

        SFXSource.volume = _SFXVolume;
        BGMSource.volume = _BGMVolume;
    }

    public void PlayEffect(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip, _SFXVolume);
    }

    public void PlayEffect(AudioClip clip, float volume = 1)
    {
        SFXSource.PlayOneShot(clip, volume);
    }

    public void PlayEffectAtPosition(AudioClip clip, Vector3 position)
    {
        SFXSource.transform.position = position;
        this.PlayEffect(clip, _SFXVolume);
    }

    public void PlayMusic(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    public void SetMusicVolume(float value)
    {
        BGMSource.volume = value;
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        SFXSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetMusicVolume(Slider slider)
    {
        BGMSource.volume = slider.value;
        PlayerPrefs.SetFloat("BGMVolume", slider.value);
    }

    public void SetSFXVolume(Slider slider)
    {
        SFXSource.volume = slider.value;
        PlayerPrefs.SetFloat("SFXVolume", slider.value);
    }
}
