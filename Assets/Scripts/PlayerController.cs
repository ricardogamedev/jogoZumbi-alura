using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IKillable
{
    private Vector3 direcao;
    public LayerMask mascaraChao;
    public GameObject textoGameOver;
    public InterfaceController scriptInterfaceController;
    public AudioClip SomDeDano;
    private PlayerMovement myPlayerMovement;
    private CharacterAnimation playerAnimation;
    public Status playerStatus;

    private void Start()
    {
        Time.timeScale = 1;
        myPlayerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<CharacterAnimation>();
        playerStatus = GetComponent<Status>();
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        direcao = new Vector3(eixoX, 0, eixoZ);

        //magnitude é o tamanho do meu vetor. Nesse caso [1,0,0] então a magnitude é 1
        playerAnimation.Movimentar(direcao.magnitude);

        if (playerStatus.Vida <= 0)
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
        myPlayerMovement.Movimentar(direcao, playerStatus.Velocidade);
        myPlayerMovement.PlayerRotation(mascaraChao);
    }

    public void TomarDano(int dano)
    {
        playerStatus.Vida -= dano;

        scriptInterfaceController.AtualizarSliderVidaJogador();
        AudioController.instancia.PlayOneShot(SomDeDano);

        if (playerStatus.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Time.timeScale = 0;
        textoGameOver.SetActive(true);
    }
}
