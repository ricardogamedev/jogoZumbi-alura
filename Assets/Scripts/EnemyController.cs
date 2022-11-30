using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float velocidade = 5;
    private Rigidbody rigidbodyEnemy;
    private Animator animatorEnemy;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
        rigidbodyEnemy = GetComponent<Rigidbody>();
        animatorEnemy = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, player.transform.position);

        Vector3 direcao = player.transform.position - transform.position;

        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidbodyEnemy.MoveRotation(novaRotacao);

        if (distancia > 2.5)
        {

            rigidbodyEnemy.MovePosition
                (rigidbodyEnemy.position +
               direcao.normalized * velocidade * Time.deltaTime);

            //Quaternions are used to represent rotations. 
            rigidbodyEnemy.MoveRotation(novaRotacao);

            animatorEnemy.SetBool("Atacando", false);

        }
        else
        {
            animatorEnemy.SetBool("Atacando", true);
        }
    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 30);
        player.GetComponent<PlayerController>().TomarDano(dano);
    }


}