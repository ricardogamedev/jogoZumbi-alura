using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour
{
    private PlayerController scriptPlayerController;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    public float tempoPontuacaoSalva;
    private int quantidadeDeZumbisMortos;
    public Text TextoQuantidadeDeZumbisMortos;

    void Start()
    {
        scriptPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        SliderVidaJogador.maxValue = scriptPlayerController.playerStatus.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoPontuacaoSalva = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptPlayerController.playerStatus.Vida;
    }

    public void AtualizarQuantidadeDeZumbisMortos()
    {
        quantidadeDeZumbisMortos ++;
        TextoQuantidadeDeZumbisMortos.text = string.Format("X {0}", quantidadeDeZumbisMortos);
    }

    public void GameOver()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)Time.timeSinceLevelLoad / 60;
        int segundos = (int)Time.timeSinceLevelLoad % 60;
        TextoTempoDeSobrevivencia.text = "Você sobreviveu por "+ minutos + " min e " + segundos + " s.";
        AjustarPontuacaoMaxima(minutos, segundos);
    }

    void AjustarPontuacaoMaxima(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalva)
        {
            tempoPontuacaoSalva = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);

            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalva);
        }
        if(TextoPontuacaoMaxima.text == "")
        {
            min = (int)tempoPontuacaoSalva / 60;
            seg = (int)tempoPontuacaoSalva % 60;

            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("motel");
    }


}
