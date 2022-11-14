using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU : MonoBehaviour
{
    public PowerUps powerUps;

    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
        powerUps.Apply(collision.gameObject);
    }
}
