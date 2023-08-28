using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    public GameObject Player;
    public GameObject Target;
    private Rigidbody2D PlayerRb;
    private PlayerController PlayerScrp;
    public float velocidad;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        PlayerScrp = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            PlayerRb.gravityScale = 0;
            PlayerScrp.velocidad = 0;
            PlayerScrp.FuerzaDeSalto = 0;
            PlayerScrp.pistolPlayer.SetActive(false);
            Player.transform.position = Vector3.Lerp(Player.transform.position, Target.transform.position, velocidad);
            PlayerScrp.playerAnim.SetBool("GG", true);
        }
    }
}
