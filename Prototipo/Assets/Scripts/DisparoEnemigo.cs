using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    public float velocidad;
    public int Direccion;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Direccion == 1)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
        else if (Direccion == 2)
        {
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        }
        else if (Direccion == 3)
        {
            transform.Translate(Vector2.down * velocidad * Time.deltaTime);
        }
    }
}
