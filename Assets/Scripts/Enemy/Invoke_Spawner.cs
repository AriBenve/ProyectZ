using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoke_Spawner : MonoBehaviour
{
    public float minKillsDisable;
    public List<Transform> spawnPoints = new List<Transform>();
    public List<GameObject> Enemies = new List<GameObject>();
    int count = 0;
    bool CR_Running = false;
    public float timeInterval;
    public int EnemiesSpawn;
    
    private void Start()
    {
        ManagerEnemy.instance.EventKillEnemy += KillEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!CR_Running)
        StartCoroutine(spawnEnemy(timeInterval, Enemies));
    }

    public IEnumerator spawnEnemy(float interval, List<GameObject> enemy)
    {


        int i = Random.Range(0, 101);
        int j = Random.Range(0, spawnPoints.Count);
        
        yield return new WaitForSeconds(interval);
        
        if(count <= EnemiesSpawn)
        {
            if (i <= 50)
            {
                GameObject newEnemy = Instantiate(enemy[0], spawnPoints[j].position, Quaternion.identity);
            }
            else if (i <= 94 && i > 50)
            {
                GameObject newEnemy = Instantiate(enemy[1], spawnPoints[j].position, Quaternion.identity);
            }
            else if (i > 94)
            {
                GameObject newEnemy = Instantiate(enemy[2], spawnPoints[j].position, Quaternion.identity);
            }

            count++;
            
        }


        StartCoroutine(spawnEnemy(timeInterval, Enemies));
    }
    
    
    public void KillEnemy(int totalKills)
    {
        Debug.Log("Llamo a mi metodo, mediante eventos");
        if (totalKills >= minKillsDisable)
        {
            ManagerEnemy.instance.EventKillEnemy -= KillEnemy;
            ManagerEnemy.instance.ResetKills();
            Destroy(gameObject);
        }
    }
    
}
