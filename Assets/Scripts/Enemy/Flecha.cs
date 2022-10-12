using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour,Idamage
{
    public float damage;

    public void Damage(float d)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var p = other.gameObject.GetComponent<Idamage>();
        
        if (p != null)
        {
            p.Damage(damage);
            

            Destroy(this.gameObject);

        }
       
    }

}
