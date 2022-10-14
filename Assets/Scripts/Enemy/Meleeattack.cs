using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Meleeattack : MonoBehaviour
{
    public float damage;

    public void Damage(float d)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print("hola");
        var p = other.gameObject.GetComponent<Idamage>();

        if (p != null)
        {
            p.Damage(damage);
        }

    }



}
