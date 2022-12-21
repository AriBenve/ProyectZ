using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public GameObject boss;

    private void Update()
    {
        if(boss == null)
        {
            this.gameObject.SetActive(false);
        }
    }
}
