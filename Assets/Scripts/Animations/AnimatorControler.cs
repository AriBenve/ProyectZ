using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControler : MonoBehaviour
{
    public Animator _animator;

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            Debug.Log("ahi");
            _animator.SetFloat("moving", 1f);
        }
        else if (Input.GetButtonUp("Vertical"))
        {
            _animator.SetFloat("moving", 0f);
            Debug.Log("me solto");
        }
    }
}
