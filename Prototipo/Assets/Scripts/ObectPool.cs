using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObectPool : MonoBehaviour
{
    public GameObject PreFabs;
    public int TamanioPool=6;
    private List<GameObject> ObjetosPool;
    // Start is called before the first frame update
    void Start()
    {
        ObjetosPool = new List<GameObject>();
        for (int i = 0; i < TamanioPool; i++) 
        {
            GameObject obj = Instantiate(PreFabs);
            obj.SetActive(false);
            ObjetosPool.Add(obj);
        }
    }

    public GameObject ObtenerObjeto() 
    {
        foreach (GameObject obj in ObjetosPool) 
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
