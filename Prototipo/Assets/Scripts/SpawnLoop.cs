using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoop : MonoBehaviour
{
    public GameObject[] PreFabs;
    public float TiempoDeSpawn;
    private ObectPool obectPool;
    // Start is called before the first frame update
    void Start()
    {
        obectPool = GetComponent<ObectPool>();
        InvokeRepeating("GenerarObjetoLoop", 1, TiempoDeSpawn);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Invocar()
    {
        Instantiate(PreFabs[0], transform.position, PreFabs[0].transform.rotation);
    }
    void GenerarObjetoLoop()
    {
        GameObject ObjetosPool = obectPool.ObtenerObjeto();
        if (ObjetosPool != null)
        {
            ObjetosPool.transform.position = transform.position;
            ObjetosPool.transform.rotation = Quaternion.identity;
            ObjetosPool.SetActive(true);
        }
    }
}
