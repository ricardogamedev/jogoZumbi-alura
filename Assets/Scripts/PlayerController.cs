using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float velocidade = 10;
    private Vector3 direcao;
    public LayerMask mascaraChao;
    public GameObject textoGameOver;
    public bool vivo = true;
    private Rigidbody rigidbodyPlayer;
    private Animator animatorPlayer;

    private void Start()
    {
        animatorPlayer = GetComponent<Animator>();
        rigidbodyPlayer = GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        direcao = new Vector3(eixoX, 0, eixoZ);

        if (direcao != Vector3.zero)
        {
            animatorPlayer.SetBool("Movendo", true);
        }
        else
        {
            animatorPlayer.SetBool("Movendo", false);
        }

        if (!vivo)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("motel");
            }
        }
    }

    //ao invés de rodar a cada frame, ele roda a cada 0,02s por padrão
    void FixedUpdate()
    {
        rigidbodyPlayer.MovePosition
            (rigidbodyPlayer.position +
            (direcao * velocidade * Time.deltaTime));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        //quando eu uso um Raycasthit, eu preciso colocar esse out no parâmetro para avisar que vai entrar no if sem valor, mas lá dentro terá
        if (Physics.Raycast(raio, out impacto, 100, mascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            rigidbodyPlayer.MoveRotation(novaRotacao);
        }
    }
}
