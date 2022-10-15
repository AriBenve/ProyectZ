using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControler : MonoBehaviour
{
    private Animator _animator;

    public float X, Z;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical");

        _animator.SetFloat("VelX", X);
        _animator.SetFloat("VelZ", Z);
    }
}
