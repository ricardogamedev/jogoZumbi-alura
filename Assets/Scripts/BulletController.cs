using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float velocidade = 20;
    // fixed update porque vou usar um Rigidbody na bala
    
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + 
            transform.forward * velocidade * Time.deltaTime);
    }
}
