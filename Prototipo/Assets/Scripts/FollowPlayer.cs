using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    private Vector3 PosicionCamara;
    private float PosicionX = 1.79f;
    // Start is called before the first frame update
    void Start()
    {
        PosicionCamara = new Vector3(PosicionX, 0, -9.9f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + PosicionCamara;
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            PosicionX = 1.79f;
        }
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            PosicionX = -1.79f;
        }
    }
}
