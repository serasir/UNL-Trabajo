using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jefe : MonoBehaviour
{
    //VELOCIDAD Y VIDA
    public float velocidad;
    public int vidaMaxima;
    public int vidaActual;
    public Image CartelDisparar;
    public GameObject BarVida;
    //OBJETOS A DISPARAR
    public GameObject[] PreFabs;
    public GameObject BarrilToxico;
    public GameObject spawn;
    public GameObject spawSuperBala;
    //SUELO Q SPAWNEA DESPUES DE MATAR AL JEFE
    public GameObject Suelo;
    //BOOLS PARA SABER SI VA A SUBIR O BAJAR
    private bool Arriba;
    private bool Abajo;
    //SCRIPT CAMARA PARA OBTENER EL BOOL DE SABER SI ESTAS EN PELEA O NO
    private FollowPlayer CamaraScrp;
    //ANIMATOR
    private Animator JefeAnim;
    //GOLPE QUE VA A HACER EL JEFE
    private int Golpe;
    private float TiempoDeEspera;
    private int repeticionDeGolpe;
    //CONTROLAR BARRA DE VIDA
    public BarraVidaJefe BarraVida;
    // Start is called before the first frame update
    void Start()
    {
        Golpe = 1;
        CamaraScrp = GameObject.Find("Main Camera").GetComponent<FollowPlayer>();
        vidaActual = vidaMaxima;
        Arriba = true;
        Abajo = false;
        JefeAnim = GameObject.Find("Jefe").GetComponent<Animator>();
        BarraVida.IniciarBarra(vidaActual);
    }

    // Update is called once per frame
    void Update()
    {
        //SUBE
        if (Arriba == true)
        {
            transform.Translate(Vector2.up * velocidad * Time.deltaTime);
        }
        //ABAJO
        else if (Abajo == true)
        {
            transform.Translate(Vector2.down * velocidad * Time.deltaTime);
        }
        if (CamaraScrp.enJefe == true)
        {
            Ataques();
            BarVida.SetActive(true);
        }
        Muerte();
    }
    private void Muerte()
    {
        if (vidaActual == 0 || vidaActual<0)
        {
            BarrilToxico.SetActive(false);
            Suelo.SetActive(true);
            BarVida.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void Ataques()
    {
        switch (Golpe)
        {
            case 1:
                StartCoroutine(Disparo());
                Golpe = 0;
                TiempoDeEspera = 9;
                repeticionDeGolpe = 2;
                StartCoroutine(Descanso());
                break;
            case 2:
                StartCoroutine(Barril());
                Golpe = 0;
                TiempoDeEspera = 5.5f;
                repeticionDeGolpe = 4;
                StartCoroutine(Descanso());
                break;
            case 3:
                StartCoroutine(SuperBala());
                Golpe = 0;
                TiempoDeEspera = 15;
                repeticionDeGolpe = 6;
                StartCoroutine(Descanso());
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //BAJA
        if (collision.CompareTag("Golpe"))
        {
            Arriba = false;
            Abajo = true;
        }
        //SUBE
        else if (collision.CompareTag("Golpe2"))
        {
            Arriba = true;
            Abajo = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //RECIBIR GOLPE + ANIMACION
        if (collision.gameObject.CompareTag("Bala"))
        {
            BarraVida.CambiarVidaActual(vidaActual);
            vidaActual -= 1;
            JefeAnim.SetBool("Golpe", true);
            StartCoroutine(TiempoDeAnimacion());
        }
    }
    IEnumerator TiempoDeAnimacion()
    {
        yield return new WaitForSeconds(1);
        JefeAnim.SetBool("Golpe", false);
        JefeAnim.SetBool("Idle", true);
    }
    //GOLPES DEL JEFE
    IEnumerator Descanso()
    {
        yield return new WaitForSeconds(TiempoDeEspera);
        BarrilToxico.SetActive(false);
        CartelDisparar.gameObject.SetActive(false);
        int GolpeRandom = Random.Range(1, 3);
        if (repeticionDeGolpe / 2 == 1)
        {
            GolpeRandom = Random.Range(2, 3);
        }
        else if (repeticionDeGolpe / 2 == 2)
        {
            GolpeRandom = 3;
        }
        else if (repeticionDeGolpe / 2 == 3) 
        {
            GolpeRandom = Random.Range(1, 2);
        }
        Golpe = GolpeRandom;
        Debug.Log(Golpe);
    }
    IEnumerator Disparo()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(2.5f);
            Instantiate(PreFabs[0], transform.position, PreFabs[0].transform.rotation);
        }
    }
    IEnumerator Barril()
    {
        for (int i = 0; i < 2; i++)
        {
            BarrilToxico.SetActive(true);
            yield return new WaitForSeconds(2f);
            Instantiate(PreFabs[1], spawn.transform.position, PreFabs[1].transform.rotation);
        }
    }
    IEnumerator SuperBala() 
    {
        CartelDisparar.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Instantiate(PreFabs[2], spawSuperBala.transform.position, PreFabs[2].transform.rotation);
    }
}
