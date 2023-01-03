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
    private Rigidbody rigidbodyPlayer;
    private Animator animatorPlayer;
    public int Vida = 100;
    public InterfaceController scriptInterfaceController;
    public AudioClip SomDeDano;

    private void Awake()
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

        if (Vida <= 0)
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

    public void TomarDano(int dano)
    {
        Vida -= dano;
        scriptInterfaceController.AtualizarSliderVidaJogador();
        AudioController.instancia.PlayOneShot(SomDeDano);

        if (Vida <= 0)
        {
            Time.timeScale = 0;
            textoGameOver.SetActive(true);
        }
    }
}
