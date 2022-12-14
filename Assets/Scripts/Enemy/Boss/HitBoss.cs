using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoss : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider coll)
    {
        var p = coll.gameObject.GetComponent<Idamage>();

        if (p != null)
        {
            p.Damage(damage);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
