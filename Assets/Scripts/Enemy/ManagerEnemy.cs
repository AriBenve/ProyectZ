using System;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemy : MonoBehaviour
{
    public static ManagerEnemy instance;

    public event Action<int> EventKillEnemy;
    int _killsEnemies;

    private void Awake()
    {
        instance = this;
    }

    public void Kill()
    {
        _killsEnemies++;
        Debug.Log("Un enemigo murio");

        EventKillEnemy?.Invoke(_killsEnemies);
    }

    public void ResetKills()
    {
        _killsEnemies = 0;
    }

}
