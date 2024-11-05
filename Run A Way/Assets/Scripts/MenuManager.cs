using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Awake()
    {
        _bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1);
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
