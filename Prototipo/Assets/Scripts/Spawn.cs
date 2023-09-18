using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] PreFabs;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Invocar",1,1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Invocar() 
    {
        Instantiate(PreFabs[0], transform.position, PreFabs[0].transform.rotation);
    }
}
