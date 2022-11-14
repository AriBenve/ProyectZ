using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthBuff")]
public class PU_Health : PowerUps
{
    public int amount;
    public override void Apply(GameObject player)
    {
        player.GetComponent<Player>().life += amount;
        Debug.Log("sumarvida");
    }
}
