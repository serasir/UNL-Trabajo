using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe : MonoBehaviour
{
    //VELOCIDAD Y VIDA
    public float velocidad;
    public int vidaMaxima;
    public int vidaActual;
    //BOOLS PARA SABER SI VA A SUBIR O BAJAR
    private bool Arriba;
    private bool Abajo;
    //ANIMATOR
    private Animator JefeAnim;
    // Start is called before the first frame update
    void Start()
    {
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
        Muerte();
    }
    private void Muerte() 
    {
        if (vidaActual == 0) 
        {
            Destroy(gameObject);
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
        yield return new WaitForSeconds(1.5f);
        JefeAnim.SetBool("Golpe", false);
    }
}
