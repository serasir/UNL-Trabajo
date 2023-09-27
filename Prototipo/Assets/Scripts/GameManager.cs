using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //PUBLIC
    public Image CartelVictoria;
    public Image CartelDerrota;
    public int nivel;
    //PRIVATE
    private PlayerController PlayerScrp;
    private Meta MetaScrp;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScrp = GameObject.Find("Player").GetComponent<PlayerController>();
        MetaScrp = GameObject.Find("Cartel").GetComponent<Meta>();
    }

    // Update is called once per frame
    void Update()
    {
        Derrota();
        Victoria();
    }
    private void Derrota() 
    {
        if (PlayerScrp.vida <= 0) 
        {
            CartelDerrota.gameObject.SetActive(true);
        }
    }
    private void Victoria() 
    {
        if (MetaScrp.Ganaste == true) 
        {
            CartelVictoria.gameObject.SetActive(true);
        }
    }
    public void ReiniciarLVL() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel() 
    {
        SceneManager.LoadScene(nivel);
    }
}
