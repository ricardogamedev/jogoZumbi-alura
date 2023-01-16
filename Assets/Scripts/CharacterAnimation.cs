using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    public void Atacar(bool isAttacking)
    {
        myAnimator.SetBool("Atacando", isAttacking);
    }

    public void Movimentar(float valorDeMovimento)
    {
        myAnimator.SetFloat("Moving", valorDeMovimento);
    }

    public void Morrer()
    {
        myAnimator.SetTrigger("Morrer");
    }
}
