using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoke_Spawner : Invoke
{
    public List<Transform> spawnPoints = new List<Transform>();
    //public List<GameObject> Enemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(spawnEnemy(timeInterval, enemies));
    }

    private void Update()
    {
    }

    public override IEnumerator spawnEnemy(float interval, GameObject[] enemy)
    {
        int i = Random.Range(0, 101);
        int j = Random.Range(0, spawnPoints.Count);
        yield return new WaitForSeconds(interval);

        if(i <= 50)
        {
            GameObject newEnemy = Instantiate(enemy[0], spawnPoints[j].position, Quaternion.identity);
        }
        else if(i <= 99 && i > 50) 
        {
            GameObject newEnemy = Instantiate(enemy[1], spawnPoints[j].position, Quaternion.identity);
        }
        else if(i > 99)
        {
            GameObject newEnemy = Instantiate(enemy[2], spawnPoints[j].position, Quaternion.identity);
        }



        StartCoroutine(spawnEnemy(timeInterval, enemies));
    }

    /*private int EnemyCount()
    {
        Enemies.Add (FindObjectOfType<Enemy>().gameObject);
    }*/
}
