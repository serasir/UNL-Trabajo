using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperBala : MonoBehaviour
{
    public float velocidad;
    public int vida;
    public int direccion;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (direccion == 1)
        {
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        }
        else if (direccion == 2)
        {
            transform.Translate(Vector2.up * velocidad * Time.deltaTime);
        }
        if (vida == 0) 
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bala")) 
        {
            vida -= 1;
        }
    }
}
