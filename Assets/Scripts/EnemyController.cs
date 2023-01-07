using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IKillable
{
    public GameObject player;
    private CharacterMovement myEnemyMovement;
    private CharacterAnimation enemyAnimation;
    private Status enemyStatus;
    public AudioClip SomDeMorte;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemyAnimation = GetComponent<CharacterAnimation>();
        myEnemyMovement = GetComponent<CharacterMovement>();
        RandomizeZombies();
        enemyStatus = GetComponent<Status>();
    }


    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, player.transform.position);

        Vector3 direcao = player.transform.position - transform.position;

        myEnemyMovement.Rotacionar(direcao);

        if (distancia > 2.5)
        {
            myEnemyMovement.Movimentar(direcao, enemyStatus.Velocidade);

            //Quaternions are used to represent rotations. 
            enemyAnimation.Atacar(false);
        }
        else
        {
            enemyAnimation.Atacar(true);
        }
    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 30);

        player.GetComponent<PlayerController>().TomarDano(dano);


    }

    void RandomizeZombies()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        enemyStatus.Vida -= dano;
        if(enemyStatus.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);
        AudioController.instancia.PlayOneShot(SomDeMorte);
    }
}
