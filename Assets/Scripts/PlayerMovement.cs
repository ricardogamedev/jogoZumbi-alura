using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
   public void PlayerRotation(LayerMask mascaraChao)
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        //quando eu uso um Raycasthit, eu preciso colocar esse out no parâmetro para avisar que vai entrar no if sem valor, mas lá dentro terá
        if (Physics.Raycast(raio, out impacto, 100, mascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Rotacionar(posicaoMiraJogador);
        }
    }
}
