using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float damage;

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
