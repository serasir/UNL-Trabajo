using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlarMenu : MonoBehaviour
{
    public Image Panel;
    public GameObject Menu;

    public Slider slider;
    public float ValorSlider;
    public Image mute;
    public Image unmute;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CambiarSlide(float valor) 
    {
        ValorSlider = valor;
        PlayerPrefs.SetFloat("volumenAudio", ValorSlider);
        AudioListener.volume = slider.value;
    }
    public void Mute() 
    {
        if (ValorSlider == 0)
        {
            mute.gameObject.SetActive(true);
            unmute.gameObject.SetActive(false);
        }
        else 
        {
            mute.gameObject.SetActive(false);
            unmute.gameObject.SetActive(true);
        }
    }
    public void EntrarOpciones() 
    {
        Menu.SetActive(false);
        Panel.gameObject.SetActive(true);
    }
    public void VolverAtras() 
    {
        Menu.SetActive(true);
        Panel.gameObject.SetActive(false);
    }
}
