using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
           
          if (gameObject.GetComponent<ObjectMovent>().willDestroy)
          {
             gameObject.GetComponent<ObjectMovent>().startCd = true;
          }
           
        }
    }
}
