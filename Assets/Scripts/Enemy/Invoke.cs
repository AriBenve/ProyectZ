using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoke : MonoBehaviour
{
    public Transform spawnPoint;

    public GameObject[] enemies = new GameObject[2];

    public int timeInterval;

    private int counter = 0;

    private void Start()
    {
        StartCoroutine(spawnEnemy(timeInterval, enemies));
    }

    public virtual IEnumerator spawnEnemy(float interval, GameObject[] enemy)
    {
        int i = Random.Range(0, enemy.Length);

        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy[i], spawnPoint.position, Quaternion.identity);

        if(counter <= 10)
        {
            counter++;
            StartCoroutine(spawnEnemy(interval, enemy));
        }
        
    }
}
