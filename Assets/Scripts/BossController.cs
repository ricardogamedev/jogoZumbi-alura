using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agente;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agente.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agente.SetDestination(player.position);
    }
}
