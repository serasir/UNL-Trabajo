using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject Player;
    private Vector2 PosicionPortal;
    // Start is called before the first frame update
    void Start()
    {
        PosicionPortal = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BalaPortal")) 
        {
            Player.transform.position = PosicionPortal;
        }
    }
}
