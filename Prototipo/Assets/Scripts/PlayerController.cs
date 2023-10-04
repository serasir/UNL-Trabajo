using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //PUBLIC
    public float velocidad;
    public float FuerzaDeSalto;
    public int vida;
    public Animator playerAnim;
    public GameObject pistolPlayer;
    public GameObject PistolaFalsa;
    public AudioClip Disparo;
    public AudioClip Death;
    public AudioClip Salto;
    public AudioClip Victoria;
    public GameObject SalidaDeBala;
    public GameObject[] PreFabBala;
    public Image Nube;
    public AudioSource SonidoPlayer;
    //PRIVATE
    private bool EstarEnSuelo;
    private float HorizontalInput;
    private Rigidbody2D playerRB;
    private int numeroSalto=0;
    private bool TenerPistola = false;
    private FollowPlayer CamaraScrp;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        SonidoPlayer = GetComponent<AudioSource>();
        CamaraScrp = GameObject.Find("Main Camera").GetComponent<FollowPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //MOVIMIENTO
        HorizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * HorizontalInput * velocidad * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerRB.AddForce(Vector2.up * FuerzaDeSalto, ForceMode2D.Impulse);
            numeroSalto++;
            EstarEnSuelo = false;
            SonidoPlayer.PlayOneShot(Salto, 1);
        }
        //MECANICA DOBLE SALTO
        else if (numeroSalto >= 2) 
        {
            FuerzaDeSalto = 0;
        }
        Animaciones();
        //APRETAR ESC PARA SALIR DEL JUEGO
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
        //DISPARAR
        if (Input.GetKeyDown(KeyCode.K) && TenerPistola == true) 
        {
            Instantiate(PreFabBala[0], SalidaDeBala.transform.position, SalidaDeBala.transform.rotation);
            SonidoPlayer.PlayOneShot(Disparo, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //CHOQUE CON ENEMIGO
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            vida -= 1;
            if (vida == 0) 
            {
                SonidoPlayer.PlayOneShot(Death, 1);
            }
        }
        //TOCAR SUELO
        else if (collision.gameObject.CompareTag("Suelo")) 
        {
            EstarEnSuelo = true;
            FuerzaDeSalto = 5;
            numeroSalto = 0;
        }
    }
    private void Animaciones() 
    {
        //SALTO
        if (EstarEnSuelo == false)
        {
            playerAnim.SetBool("Saltando", true);
        }
        else if (EstarEnSuelo == true) 
        {
            playerAnim.SetBool("Saltando", false);
        }
        //CAMINAR
        if (Input.GetKey(KeyCode.D))
        {
            playerAnim.SetBool("Caminando", true);
        }
        else if (!Input.GetKey(KeyCode.D)) 
        {
            playerAnim.SetBool("Caminando", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerAnim.SetBool("CamIzq", true);
        }
        else if (!Input.GetKey(KeyCode.A)) 
        {
            playerAnim.SetBool("CamIzq", false);
        }
        //MUERTE
        if (vida <= 0)
        {

            playerAnim.SetBool("Muerto", true);
            StartCoroutine(TiempoParaCambiarAnimacion());
            velocidad = 0;
            FuerzaDeSalto = 5;
        }
    }
    IEnumerator TiempoParaCambiarAnimacion() 
    {
        yield return new WaitForSeconds(0.58f);
        playerAnim.SetBool("Muerto", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //POR AHORA NO SE UTILIZA ESTO
        if (collision.CompareTag("cartel"))
        {
            Nube.gameObject.SetActive(true);
            StartCoroutine(TiempoParaApagarCartel());
        }
        else if (collision.CompareTag("Entrada"))
        {
            CamaraScrp.enJefe = true;
            Debug.Log("comova");
        }
        else if (collision.CompareTag("Pistola"))
        {
            pistolPlayer.SetActive(true);
            Destroy(PistolaFalsa);
            TenerPistola = true;
        }
        else if (collision.CompareTag("Enemigo")) 
        {
            vida -= 1;
            if (vida == 0)
            {
                SonidoPlayer.PlayOneShot(Death, 1);
            }
        }
    }
    IEnumerator TiempoParaApagarCartel() 
    {
        yield return new WaitForSeconds(2);
        Nube.gameObject.SetActive(false);
    }
}
