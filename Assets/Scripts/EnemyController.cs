using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float velocidade = 5;
    private Animator animatorEnemy;
    private CharacterMovement myEnemyMovement;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
        animatorEnemy = GetComponent<Animator>();
        myEnemyMovement = GetComponent<CharacterMovement>();
    }


    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, player.transform.position);

        Vector3 direcao = player.transform.position - transform.position;

        myEnemyMovement.Rotacionar(direcao);

        if (distancia > 2.5)
        {
            myEnemyMovement.Movimentar(direcao, velocidade);

            //Quaternions are used to represent rotations. 
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
