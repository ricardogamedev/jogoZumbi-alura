using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float velocidade = 20;
    private Rigidbody rigidbodybullet;
    // fixed update porque vou usar um Rigidbody na bala

    private void Start()
    {
        rigidbodybullet = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rigidbodybullet.MovePosition(rigidbodybullet.position +
            transform.forward * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Inimigo")
        {
            Destroy(objetoDeColisao.gameObject);
        }
        //gameObject minúsculo pega quem está com o script 
        Destroy(gameObject);
    }
}
