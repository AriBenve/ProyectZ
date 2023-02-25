using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float damage;
    float Timer = 0;
    float DestroyCD;

    private void Start()
    {
        DestroyCD = Timer ;
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
    
    private void Update()
    {
        Timer += Time.deltaTime;
        if(DestroyCD >= 20)
        {
            Destroy(this.gameObject);
        }

    }
    
}
