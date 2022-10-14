using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,Idamage
{
    public float _life;

    private void Start()
    {
        _life = 100;
    }

    private void Update()
    {
        if(_life <= 0)
        {
            Death();
        }
    }

    public void Damage(float d)
    {
        _life -= d;
        Debug.Log(_life);
    }

    public void Death()
    {
        Debug.Log("Me fulminaron wachin que carajo hiciste pedazo de aweonado");
    }
}
