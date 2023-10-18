using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad;
    private bool arriba;
    private bool izquierda;
    private bool abajo;
    private bool derecha;
    // Start is called before the first frame update
    void Start()
    {
        arriba = true;
        izquierda = false;
        abajo = false;
        derecha = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (arriba == true) 
        {
            transform.Translate(Vector2.up * velocidad * Time.deltaTime);
        }
        if (izquierda == true) 
        {
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        }
        if (abajo == true) 
        {
            transform.Translate(Vector2.down * velocidad * Time.deltaTime);
        }
        if (derecha == true) 
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Referencia(Fondo)"))
        {
            arriba = false;
            izquierda = true;
        }
        else if (collision.CompareTag("Referencia(Fondo2)"))
        {
            arriba = false;
            izquierda = false;
            abajo = true;
        }
        else if (collision.CompareTag("Referencia(Fondo3)"))
        {
            arriba = false;
            izquierda = false;
            abajo = false;
            derecha = true;
        }
        else if (collision.CompareTag("Referencia(Fondo4)")) 
        {
            arriba = true;
            izquierda = false;
            abajo = false;
            derecha = false;
        }
    }
}
