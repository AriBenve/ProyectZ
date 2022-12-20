using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Boss : MonoBehaviour
{
    public GameObject player;
    public Transform TP;

    private void OnCollisionEnter(Collision collision)
    {
        player.transform.position = TP.position;
    }
}
