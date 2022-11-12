using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int minKillsOpen;

    private void Start()
    {
        ManagerEnemy.instance.EventKillEnemy += KillEnemy;
    }

    public void KillEnemy(int totalKills)
    {
        Debug.Log("Llamo a mi metodo, mediante eventos");
        if (totalKills >= minKillsOpen)
        {
            ManagerEnemy.instance.EventKillEnemy -= KillEnemy;
            ManagerEnemy.instance.ResetKills();
            Destroy(gameObject);
        }
    }
}
