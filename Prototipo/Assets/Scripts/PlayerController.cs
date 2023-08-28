using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject SalidaDeBala;
    public GameObject[] PreFabBala;
    public Image Nube;
    private float HorizontalInput;
    public float velocidad;
    public float FuerzaDeSalto;
    private Rigidbody2D playerRB;
    public int vida;
    public Animator playerAnim;
    private int numeroSalto=0;
    public GameObject pistolPlayer;
    public GameObject PistolaFalsa;
    private bool TenerPistola = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * HorizontalInput * velocidad * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerRB.AddForce(Vector2.up * FuerzaDeSalto, ForceMode2D.Impulse);
            numeroSalto++;
        }
        else if (numeroSalto >= 2) 
        {
            FuerzaDeSalto = 0;
        }
        Muerte();
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.K) && TenerPistola == true) 
        {
            Instantiate(PreFabBala[0], SalidaDeBala.transform.position, SalidaDeBala.transform.rotation);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            vida -= 1;
            Debug.Log(vida);
        }
        else if (collision.gameObject.CompareTag("Suelo")) 
        {
            FuerzaDeSalto = 4.5f;
            numeroSalto = 0;
        }
    }
    private void Muerte() 
    {
        if (vida <= 0) 
        {
            playerAnim.SetBool("Muerto", true);
            StartCoroutine(TiempoParaCambiarAnimacion());
            velocidad = 0;
            FuerzaDeSalto = 0;
        }
    }
    IEnumerator TiempoParaCambiarAnimacion() 
    {
        yield return new WaitForSeconds(0.58f);
        playerAnim.SetBool("Muerto", false);
        playerAnim.SetBool("Bien Muerto",true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("cartel"))
        {
            Nube.gameObject.SetActive(true);
            StartCoroutine(TiempoParaApagarCartel());
        }
        else if (collision.CompareTag("Pistola")) 
        {
            pistolPlayer.SetActive(true);
            Destroy(PistolaFalsa);
            TenerPistola = true;
        }
    }
    IEnumerator TiempoParaApagarCartel() 
    {
        yield return new WaitForSeconds(2);
        Nube.gameObject.SetActive(false);
    }
}