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
    public Image Logro;
    public int nivel;
    public int CantidadDeLogros;
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
        GuardarNumero();
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
            Logro.gameObject.SetActive(true);
            StartCoroutine(TiempoDeLogro());
            CartelVictoria.gameObject.SetActive(true);
            CantidadDeLogros +=1;
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
    public void GuardarNumero() 
    {
        PlayerPrefs.SetInt("Logros", CantidadDeLogros);
        PlayerPrefs.Save();
    }
    IEnumerator TiempoDeLogro() 
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(Logro.gameObject);
    }
}
