using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<Player>().Death();
        }
    }
}
