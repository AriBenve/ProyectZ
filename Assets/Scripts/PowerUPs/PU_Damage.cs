using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Damage")]
public class PU_Damage : PowerUps
{
    public float damage;
   
    public override void Apply(GameObject player)
    {
        var p = player.gameObject.GetComponent<Idamage>();

        if (p != null)
        {
            p.Damage(damage);
            
        }
    }
}
