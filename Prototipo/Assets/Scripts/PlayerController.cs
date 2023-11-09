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
    public GameObject PistolaPortal;
    public GameObject PistolaPortalFalsa;
    public AudioClip Disparo;
    public AudioClip Death;
    public AudioClip Salto;
    public AudioClip Victoria;
    public GameObject SalidaDeBala;
    public GameObject[] PreFabBala;
    public Image Nube;
    public AudioSource SonidoPlayer;
    public Image HudArma;
    public Image PistolaHUD;
    public Image PortalHUD;
    //PRIVATE
    private bool EstarEnSuelo;
    private float HorizontalInput;
    private Rigidbody2D playerRB;
    private int numeroSalto=0;
    private bool TenerPistola = false;
    private bool TenerPistolaPortal = false;
    private bool CambiarPistola = false;
    private FollowPlayer CamaraScrp;
    private GameManager gameManager;
    private Jefe2 boss;
    //CODIGO PARA DIFERENTES SKINS COMO RECOMPENSAS DE LOGROS
    public AnimatorOverrideController BlackSkin;
    private RuntimeAnimatorController OriginalSkin;
    private int numeroDeLogros;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        OriginalSkin = playerAnim.runtimeAnimatorController;
        SonidoPlayer = GetComponent<AudioSource>();
        CamaraScrp = GameObject.Find("Main Camera").GetComponent<FollowPlayer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        numeroDeLogros = PlayerPrefs.GetInt("Logros");
        boss = GameObject.Find("JEFE 2").GetComponent<Jefe2>();
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
        //CAMBIAR ARMA Y DISPARAR
        if (TenerPistola==true && Input.GetKeyDown(KeyCode.Alpha1))
        {
            PistolaHUD.gameObject.SetActive(true);
            PortalHUD.gameObject.SetActive(false);
            CambiarPistola = false;
            pistolPlayer.SetActive(true);
            PistolaPortal.SetActive(false);
        }
        else if (TenerPistolaPortal==true && Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            PistolaHUD.gameObject.SetActive(false);
            PortalHUD.gameObject.SetActive(true);
            CambiarPistola = true;
            PistolaPortal.SetActive(true);
            pistolPlayer.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.K) && TenerPistola == true && CambiarPistola==false) 
        {
            Instantiate(PreFabBala[0], SalidaDeBala.transform.position, SalidaDeBala.transform.rotation);
            SonidoPlayer.PlayOneShot(Disparo, 1);
        }
        if (Input.GetKeyDown(KeyCode.K) && TenerPistolaPortal == true && CambiarPistola==true) 
        {
            Instantiate(PreFabBala[1], SalidaDeBala.transform.position, SalidaDeBala.transform.rotation);
            SonidoPlayer.PlayOneShot(Disparo, 1);
        }
        //CAMBIAR SKIN
        if (Input.GetKeyDown(KeyCode.J)&& numeroDeLogros>1) 
        {
            playerAnim.runtimeAnimatorController = BlackSkin as RuntimeAnimatorController;
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
        //OBTENER PISTOLA
        else if (collision.CompareTag("Pistola"))
        {
            HudArma.gameObject.SetActive(true);
            PistolaHUD.gameObject.SetActive(true);
            PortalHUD.gameObject.SetActive(false);
            pistolPlayer.SetActive(true);
            PistolaPortal.SetActive(false);
            Destroy(PistolaFalsa);
            TenerPistola = true;
            CambiarPistola = false;
        }
        //OBTENER PORTALGUN
        else if (collision.CompareTag("PortalGun")) 
        {
            HudArma.gameObject.SetActive(true);
            PistolaHUD.gameObject.SetActive(false);
            PortalHUD.gameObject.SetActive(true);
            PistolaPortal.SetActive(true);
            pistolPlayer.SetActive(false);
            Destroy(PistolaPortalFalsa);
            TenerPistolaPortal = true;
            CambiarPistola = true;
        }
        else if (collision.CompareTag("Enemigo"))
        {
            vida -= 1;
            if (vida == 0)
            {
                SonidoPlayer.PlayOneShot(Death, 1);
            }
        }
        if (collision.CompareTag("TriggerJefe")) 
        {
            boss.EnPelea = true;
        }
    }
    IEnumerator TiempoParaApagarCartel() 
    {
        yield return new WaitForSeconds(2);
        Nube.gameObject.SetActive(false);
    }
}
