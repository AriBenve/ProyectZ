using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        var p = other.gameObject.GetComponent<Idamage>();
        var e = other.gameObject.GetComponent<Enemy>();
        
        if (p != null)
        {
            p.Damage(damage);
            Destroy(this.gameObject);
        }
        else if(e != null)
        {

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
