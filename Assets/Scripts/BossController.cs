using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agente;
    private Status statusChefe;
    private CharacterAnimation animacaoChefe;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        agente.speed = statusChefe.Velocidade;
        animacaoChefe = GetComponent<CharacterAnimation>();
    }

    private void Update()
    {
        agente.SetDestination(player.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;

        if (estouPertoDoJogador)
        {
            animacaoChefe.Atacar(true);
        }else
        {
            animacaoChefe.Atacar(false);
        }
    }
}
