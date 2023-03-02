using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int minKillsOpen;
    [SerializeField] AudioSource AS;

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
            AudioSource.PlayClipAtPoint(AS.clip, this.gameObject.transform.position);
            this.gameObject.SetActive(false);
        }
    }
}
