using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRojo : MonoBehaviour
{
    public GameObject portalAlQueHacerTP;
    private Vector2 PosicionPortalTP;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PosicionPortalTP = portalAlQueHacerTP.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BalaPortal")) 
        {
            player.transform.position=PosicionPortalTP; 
        }
    }
}
