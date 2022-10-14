using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoke : MonoBehaviour
{
    public Transform spawnPoint;

    [SerializeField]
    private GameObject[] enemies = new GameObject[2];

    [SerializeField]
    private int timeInterval;

    private void Start()
    {
        StartCoroutine(spawnEnemy(timeInterval, enemies));
    }

    private IEnumerator spawnEnemy(float interval, GameObject[] enemy)
    {
        int i = Random.Range(0, enemy.Length);

        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy[i], spawnPoint.position, Quaternion.identity);

        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
