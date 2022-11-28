﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidade = 10;
    Vector3 direcao;
    public LayerMask mascaraChao;

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        direcao = new Vector3(eixoX, 0, eixoZ);

        if (direcao != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("Movendo", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Movendo", false);
        }
    }

    //ao invés de rodar a cada frame, ele roda a cada 0,02s por padrão
     void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition
            (GetComponent<Rigidbody>().position + 
            (direcao * velocidade * Time.deltaTime));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        //quando eu uso um Raycasthit, eu preciso colocar esse out no parâmetro para avisar que vai entrar no if sem valor, mas lá dentro terá
        if(Physics.Raycast(raio, out impacto, 100, mascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }
}
