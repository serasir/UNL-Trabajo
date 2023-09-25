using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe : MonoBehaviour
{
    public float velocidad;
    public int vida;
    private bool Arriba;
    private bool Abajo;
    private Animator JefeAnim;
    // Start is called before the first frame update
    void Start()
    {
        Arriba = true;
        Abajo = false;
        JefeAnim = GameObject.Find("Jefe").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Arriba == true)
        {
            transform.Translate(Vector2.up * velocidad * Time.deltaTime);
        }
        else if (Abajo == true) 
        {
            transform.Translate(Vector2.down * velocidad * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Golpe"))
        {
            Arriba = false;
            Abajo = true;
        }
        else if (collision.CompareTag("Golpe2")) 
        {
            Arriba = true;
            Abajo = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala")) 
        {
            vida -= 1;
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
