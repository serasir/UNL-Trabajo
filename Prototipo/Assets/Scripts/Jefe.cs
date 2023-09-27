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
    public Image BarraDeVidas;
    //OBJETOS A DISPARAR
    public GameObject[] PreFabs;
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
    // Start is called before the first frame update
    void Start()
    {
        Golpe = 1;
        CamaraScrp = GameObject.Find("Main Camera").GetComponent<FollowPlayer>();
        vidaActual = vidaMaxima;
        Arriba = true;
        Abajo = false;
        JefeAnim = GameObject.Find("Jefe").GetComponent<Animator>();
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
            BarraDeVidas.gameObject.SetActive(true);
            BarraDeVidas.fillAmount=vidaActual / vidaMaxima;
        }
        Muerte();
    }
    private void Muerte() 
    {
        if (vidaActual == 0) 
        {
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
                StartCoroutine(Descanso());
                break;
            case 2:
                
                Golpe = 0;
                StartCoroutine(Descanso());
                break;
            case 3:
                Golpe = 0;
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
        Debug.Log("god");
        int GolpeRandom = Random.Range(0, 3);
        Golpe = GolpeRandom;
    }
    IEnumerator Disparo() 
    {
        for (int i = 0; i < 3; i++) 
        {
            yield return new WaitForSeconds(2.5f);
            Instantiate(PreFabs[0], transform.position, PreFabs[0].transform.rotation);
        }
    }
}
