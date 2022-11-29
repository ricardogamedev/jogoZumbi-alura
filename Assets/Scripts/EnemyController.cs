using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float velocidade = 5;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, player.transform.position);

        Vector3 direcao = player.transform.position - transform.position;

        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        GetComponent<Rigidbody>().MoveRotation(novaRotacao);

        if (distancia > 2.5)
        {

            GetComponent<Rigidbody>().MovePosition
                (GetComponent<Rigidbody>().position +
               direcao.normalized * velocidade * Time.deltaTime);

            //Quaternions are used to represent rotations. 
            GetComponent<Rigidbody>().MoveRotation(novaRotacao);

            GetComponent<Animator>().SetBool("Atacando", false);

        }
        else
        {
            GetComponent<Animator>().SetBool("Atacando", true);
        }
    }

    void AtacaJogador()
    {
        Time.timeScale = 0;
        player.GetComponent<PlayerController>().textoGameOver.SetActive(true);
        player.GetComponent<PlayerController>().vivo = false;
    }


}

