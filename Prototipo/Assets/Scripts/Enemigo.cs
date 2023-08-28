using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float vida = 1;
    private Animator EnemyAnim;
    public float velocidad;
    private bool Cambiar=false;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAnim = GameObject.Find("Enemigo").GetComponent<Animator>();

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
        muerte();
    }
    private void muerte() 
    {
        if (vida <= 0) 
        {
            velocidad = 0;
            EnemyAnim.SetBool("Muerto", true);
            StartCoroutine(TiempoParaMorir());
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
            Cambiar=false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            vida -= 1;
        }
    }
    IEnumerator TiempoParaMorir() 
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }

}
