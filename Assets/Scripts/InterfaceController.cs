using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    private PlayerController scriptPlayerController;
    public Slider SliderVidaJogador;
    void Start()
    {
        scriptPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        SliderVidaJogador.maxValue = scriptPlayerController.playerStatus.Vida;
        AtualizarSliderVidaJogador();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptPlayerController.playerStatus.Vida;
    }
}
