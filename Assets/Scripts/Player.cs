using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,Idamage
{
    public float _life;

    public void Damage(float d)
    {
        _life -= d;
    }

    public void takedamage(float d)
    {
        
    }
}
