using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControler : MonoBehaviour
{
    public Animator _animator;

    private void Awake()
    {
        _animator.SetTrigger("ataque");
    }

    private void Update()
    {

    }
}
