using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControler : MonoBehaviour
{
    Animator _animator;

    private Vector3 _direccion;

    void Update()
    {
        _direccion.x = Input.GetAxis("Horizontal");
        _direccion.z = Input.GetAxis("Vertical");

        _animator.SetFloat("zAxi", _direccion.z);
    }
}
