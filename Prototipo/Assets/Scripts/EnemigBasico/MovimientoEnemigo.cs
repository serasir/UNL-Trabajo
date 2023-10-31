using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public float velocidad;
    private bool Cambiar = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cambiar == false)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
        else if (Cambiar == true)
        {
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Golpe"))
        {
            Cambiar = true;
        }
        else if (collision.CompareTag("Golpe2"))
        {
            Cambiar = false;
        }
    }
}
