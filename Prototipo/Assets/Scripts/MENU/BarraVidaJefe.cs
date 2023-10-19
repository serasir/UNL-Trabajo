using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVidaJefe : MonoBehaviour
{
    private Slider Barra;
    // Start is called before the first frame update
    void Start()
    {
        Barra= GetComponent<Slider>();
    }

    public void CambiarVidaMaxima(float vidaMaxima) 
    {
        Barra.maxValue= vidaMaxima;
    }
    public void CambiarVidaActual(float vidaActual) 
    {
        Barra.value= vidaActual;
    }
    public void IniciarBarra(float vidaActual) 
    {
        CambiarVidaMaxima(vidaActual);
        CambiarVidaActual(vidaActual);
    }
}
