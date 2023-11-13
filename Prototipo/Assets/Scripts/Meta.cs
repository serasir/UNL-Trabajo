using UnityEngine;

public class Meta : MonoBehaviour
{
    public GameObject Player;
    public GameObject Target;
    private Rigidbody2D PlayerRb;
    private PlayerController PlayerScrp;
    public float velocidad;
    private AudioSource MusicaFondo;
    public bool Ganaste;
    // Start is called before the first frame update
    void Start()
    {
        MusicaFondo = GameObject.Find("MusicaFondo").GetComponent<AudioSource>();
        PlayerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        PlayerScrp = GameObject.Find("Player").GetComponent<PlayerController>();
        Ganaste = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Ganaste = true;
            PlayerRb.gravityScale = 0;
            PlayerScrp.velocidad = 0;
            PlayerScrp.FuerzaDeSalto = 0;
            PlayerScrp.pistolPlayer.SetActive(false);
            Player.transform.position = Vector3.Lerp(Player.transform.position, Target.transform.position, velocidad);
            PlayerScrp.playerAnim.SetBool("GG", true);
            PlayerScrp.SonidoPlayer.PlayOneShot(PlayerScrp.Victoria, 1);
            MusicaFondo.Stop();     
        }
    }
}
