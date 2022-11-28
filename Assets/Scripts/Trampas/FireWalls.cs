using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWalls : MonoBehaviour
{
    public float dmg;
    public float timer;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            StartCoroutine(player.GraduallyReduceHP(dmg, timer));
        }
    }
}
