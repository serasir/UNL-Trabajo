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
    
    //PRIVATE
    private PlayerController PlayerScrp;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScrp = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Derrota();
    }
    private void Derrota() 
    {
        if (PlayerScrp.vida <= 0) 
        {
            CartelDerrota.gameObject.SetActive(true);
        }
    }
    public void ReiniciarLVL() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
