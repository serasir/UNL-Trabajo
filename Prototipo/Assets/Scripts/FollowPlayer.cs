using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    private Vector3 PosicionCamara;
    private float PosicionX = 1.79f;
    public bool enJefe;
    public Camera Camara2d;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        PosicionCamara = new Vector3(PosicionX, 0, -9.9f);
        transform.position = Player.transform.position + PosicionCamara;
        if (enJefe == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                PosicionX = 1.79f;
            }
        }
        else if (enJefe == true) 
        {
            Camara2d.orthographicSize = 9; 
            PosicionX = 12.50f;
        }
    }
}
