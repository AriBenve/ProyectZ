using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoke_Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    public List<GameObject> Enemies = new List<GameObject>();
    int count = 0;
    bool CR_Running = false;
    public float timeInterval;
    

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
        
        if(count <= 30)
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

    /*private int EnemyCount()
    {
        Enemies.Add (FindObjectOfType<Enemy>().gameObject);
    }*/
}
