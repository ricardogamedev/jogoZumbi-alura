using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource meuAudioSource;
    public static AudioSource instancia;

     void Awake()
    {

        meuAudioSource = GetComponent<AudioSource>();
        instancia = meuAudioSource;
    }
}
