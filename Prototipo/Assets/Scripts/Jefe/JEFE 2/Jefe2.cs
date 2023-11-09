using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe2 : MonoBehaviour
{
    //PRIVATE
    private int Peligro;
    private int Golpe;
    private int TiempoDeEspera;
    //PUBLIC
    public int VidaMaxima;
    public int VidaActual;
    public bool EnPelea;
    public GameObject BarVida;
    public BarraVidaJefe BarraVida;
    public GameObject PosicionInicial;
    //PRIMER GOLPE
    public GameObject Arriba;
    public GameObject SueloArriba;
    public GameObject PortalesArriba;
    public GameObject Spikes;
    public GameObject Warning1;
    public GameObject Warning2;
    public GameObject WarningGeneral;
    public GameObject SpikeArriba1;
    public GameObject SpikeArriba2;


    //SEGUNDO GOLPE
    public GameObject Guia1;
    public GameObject Guia2;
    //ANGULO Z DESEADO PARA ROTAR
    private float nuevoAnguloZ = 53.5f;
    private float SegundoAnguloz = -214.917f;
    private float AnguloZ = 0;
    public GameObject[] PreFab;
    // Start is called before the first frame update
    void Start()
    {
        Golpe = 2;
        VidaActual = VidaMaxima;
        BarraVida.IniciarBarra(VidaActual);
    }


    // Update is called once per frame
    void Update()
    {
        if (EnPelea == true) 
        {
            Ataques();
            BarVida.SetActive(true);
        }
        Muerto();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala")) 
        {
            VidaActual -= 1;
            BarraVida.CambiarVidaActual(VidaActual);
        }
    }
    private void Ataques() 
    {
        switch (Golpe) 
        {
            case 1:
                StartCoroutine(TPS());
                Golpe = 0;
                TiempoDeEspera = 6;
                StartCoroutine(Descanso());
                break;
            case 2:
                StartCoroutine(Disaparo());
                Golpe = 0;
                TiempoDeEspera=5;
                StartCoroutine(Descanso());
                break;
            case 3:
                break;

        }
    }
    private void Muerto() 
    {
        if (VidaActual == 0) 
        {
            Destroy(gameObject);
            BarVida.SetActive(false);
        }
    }
    IEnumerator Descanso() 
    {
        yield return new WaitForSeconds(TiempoDeEspera);
        //DESACTIVAR
        transform.position = PosicionInicial.transform.position;
        SueloArriba.SetActive(false);
        PortalesArriba.SetActive(false);
        Warning1.SetActive(false);
        Warning2.SetActive(false);
        WarningGeneral.SetActive(false);
        Spikes.SetActive(false);
        SpikeArriba1.SetActive(false);
        SpikeArriba2.SetActive(false);
        Vector3 nuevaRotacion = transform.rotation.eulerAngles;
        nuevaRotacion.z = AnguloZ;
        transform.rotation = Quaternion.Euler(nuevaRotacion);
        yield return new WaitForSeconds(2.5f);
        int GolpeRandom = Random.Range(1, 3);
        Golpe = GolpeRandom;
    }
    IEnumerator TPS() 
    {
        Peligro = Random.Range(1, 2);
        transform.position = Arriba.transform.position;
        SueloArriba.SetActive(true);
        PortalesArriba.SetActive(true);
        WarningGeneral.SetActive(true);
        if (Peligro == 1) 
        {
            Warning1.SetActive(true);
        }
        if (Peligro == 2) 
        {
            Warning2.SetActive(true);
        }
        yield return new WaitForSeconds(3);
        Spikes.SetActive(true);
        if (Peligro == 1)
        {
            SpikeArriba1.SetActive(true);
        }
        if (Peligro == 2) 
        {
            SpikeArriba2.SetActive(true);
        }
    }
    IEnumerator Disaparo() 
    {
        transform.position = Guia1.transform.position;
        //OBTENER ROTACION ACTUAL
        Vector3 nuevaRotacion = transform.rotation.eulerAngles;
        //CAMBIAR EL VALOR Z
        nuevaRotacion.z = nuevoAnguloZ;
        //ROTAR
        transform.rotation = Quaternion.Euler(nuevaRotacion);
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 1; i++) 
        {
            Instantiate(PreFab[0], transform.position, PreFab[0].transform.rotation);
        }
        transform.position = Guia2.transform.position;
        nuevaRotacion.z = SegundoAnguloz;
        transform.rotation = Quaternion.Euler(nuevaRotacion);
        yield return new WaitForSeconds(2);
        for (int j = 0; j < 1; j++) 
        {
            Instantiate(PreFab[1], transform.position, PreFab[1].transform.rotation);
        }
    }
}
