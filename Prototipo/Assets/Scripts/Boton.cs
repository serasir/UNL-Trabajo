using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    private Animator PuertaAnim;
    public BoxCollider2D PuertaCollider;
    private Animator BotonAnim;
    // Start is called before the first frame update
    void Start()
    {
        PuertaAnim = GameObject.Find("Puerta").GetComponent<Animator>();
        BotonAnim = GameObject.Find("Boton").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            BotonAnim.SetBool("Activando", true);
            PuertaAnim.SetBool("Abriendo", true);
            PuertaCollider.enabled = false;
        }
    }
}
