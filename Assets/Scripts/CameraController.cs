using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 distanciaDoPlayer;
   
    void Start()
    {
        distanciaDoPlayer = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + distanciaDoPlayer;
        
    }
}
