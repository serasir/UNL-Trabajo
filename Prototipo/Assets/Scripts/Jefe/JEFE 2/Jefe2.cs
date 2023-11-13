using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe2 : MonoBehaviour
{
    //PRIVATE
    private int Peligro;
    private int Golpe;
    private int TiempoDeEspera;
    private int Repeticion1;
    private int Repeticion2;
    private int Repeticion3;
    //PUBLIC
    public int VidaMaxima;
    public int VidaActual;
    public bool EnPelea;
    public GameObject BarVida;
    public BarraVidaJefe BarraVida;
    public GameObject PosicionInicial;
    public GameObject PortalFinal;
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

    //TERCER GOLPE
    public GameObject Portal3ATK;
    public GameObject SueloMedio;
    public GameObject TechoMedio;
    public GameObject PosicionTercerAtaque;
    //ANGULO Z DESEADO PARA ROTAR
    private float nuevoAnguloZ = 53.5f;
    private float SegundoAnguloz = -214.917f;
    private float AnguloZ = 0;
    public GameObject[] PreFab;
    // Start is called before the first frame update
    void Start()
    {
        Golpe = 3;
        VidaActual = VidaMaxima;
        BarraVida.IniciarBarra(VidaActual);
        Repeticion1 = 0;
        Repeticion2 = 0;
        Repeticion3 = 0;
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
                Repeticion1++;
                Golpe = 0;
                TiempoDeEspera = 6;
                StartCoroutine(Descanso());
                break;
            case 2:
                StartCoroutine(Disaparo());
                Repeticion2++;
                Golpe = 0;
                TiempoDeEspera=5;
                StartCoroutine(Descanso());
                break;
            case 3:
                StartCoroutine(SuperBala());
                Repeticion3++;
                Golpe = 0;
                TiempoDeEspera = 20;
                StartCoroutine(Descanso());
                break;
            case 4:
                Golpe = 0;
                TiempoDeEspera = 1;
                StartCoroutine(Descanso());
                break;

        }
    }
    private void Muerto() 
    {
        if (VidaActual == 0) 
        {
            Destroy(gameObject);
            BarVida.SetActive(false);
            PortalFinal.SetActive(true);
            SueloArriba.SetActive(false);
            PortalesArriba.SetActive(false);
            Warning1.SetActive(false);
            Warning2.SetActive(false);
            WarningGeneral.SetActive(false);
            Spikes.SetActive(false);
            SpikeArriba1.SetActive(false);
            SpikeArriba2.SetActive(false);
            Portal3ATK.SetActive(false);
            SueloMedio.SetActive(false);
            TechoMedio.SetActive(false);
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
        Portal3ATK.SetActive(false);
        SueloMedio.SetActive(false);
        TechoMedio.SetActive(false);
        Vector3 nuevaRotacion = transform.rotation.eulerAngles;
        nuevaRotacion.z = AnguloZ;
        transform.rotation = Quaternion.Euler(nuevaRotacion);
        yield return new WaitForSeconds(2.5f);
        int GolpeRandom = Random.Range(1, 4);
        Golpe = GolpeRandom;
        if (Repeticion2 == 2)
        {
            Repeticion2 = 0;
            Golpe = 1;
        }
        else if (Repeticion1 == 2)
        {
            Golpe = Random.Range(2, 4);
            Repeticion1 = 0;
        }
        else if (Repeticion3 == 2) 
        {
            Golpe = Random.Range(1, 4);
            Repeticion3 = 0;
        }
        Debug.Log(Golpe);
    }
    IEnumerator TPS() 
    {
        Peligro = Random.Range(1, 3);
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
    IEnumerator SuperBala() 
    {
        transform.position = PosicionTercerAtaque.transform.position;
        Portal3ATK.SetActive(true);
        SueloMedio.SetActive(true);
        TechoMedio.SetActive(true);
        WarningGeneral.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        Spikes.SetActive(true);
        Instantiate(PreFab[2], transform.position, PreFab[2].transform.rotation);
    }
}
