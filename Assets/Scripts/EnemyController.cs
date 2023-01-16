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
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;
    private float porcentagemGerarKitMedico = 0.1f;
    public GameObject KitMedicoPrefab;
    private InterfaceController scriptInterfaceController;
    [HideInInspector]
    public ZombieGenerator meuGerador;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemyAnimation = GetComponent<CharacterAnimation>();
        myEnemyMovement = GetComponent<CharacterMovement>();
        RandomizeZombies();
        enemyStatus = GetComponent<Status>();
        scriptInterfaceController = GameObject.FindObjectOfType(typeof(InterfaceController)) as InterfaceController;
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, player.transform.position);

        myEnemyMovement.Rotacionar(direcao);
        enemyAnimation.Movimentar(direcao.magnitude);

        if (distancia > 15)
        {
            Vagar();
        }

        else if (distancia > 2.5)
        {
            direcao = player.transform.position - transform.position;
            myEnemyMovement.Movimentar(direcao, enemyStatus.Velocidade);

            //Quaternions are used to represent rotations. 
            enemyAnimation.Atacar(false);
        }
        else
        {
            direcao = player.transform.position - transform.position;
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
        if (enemyStatus.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject, 2);
        enemyAnimation.Morrer();
        myEnemyMovement.Morrer();
        this.enabled = false;
        AudioController.instancia.PlayOneShot(SomDeMorte);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptInterfaceController.AtualizarQuantidadeDeZumbisMortos();
        meuGerador.DiminuirQuantidadeDeZumbisVivos();
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);

        }
        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;

        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            myEnemyMovement.Movimentar(direcao, enemyStatus.Velocidade);
        }
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
}
