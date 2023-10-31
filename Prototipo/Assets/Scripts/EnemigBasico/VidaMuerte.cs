using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaMuerte : MonoBehaviour
{
    public float vida = 1;
    private Animator EnemyAnim;
    private MovimientoEnemigo Movimiento;
    // Start is called before the first frame update
    void Start()
    {
        Movimiento = GameObject.Find("Enemigo").GetComponent<MovimientoEnemigo>();
    }

    // Update is called once per frame
    void Update()
    {
        muerte();
    }
    private void muerte()
    {
        if (vida <= 0)
        {
            Movimiento.velocidad = 0;
            EnemyAnim.SetBool("Muerto", true);
            StartCoroutine(TiempoParaMorir());
        }
    }
    IEnumerator TiempoParaMorir()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
}
