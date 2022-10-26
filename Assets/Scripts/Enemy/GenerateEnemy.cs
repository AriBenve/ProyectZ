using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    //public GameObject Enemy;
    public GameObject[] Enemy = new GameObject[3];

    public int xPos;
    public int zPos;
    public int EnemyCount;


    void Start()
    {
        StartCoroutine(Enemyrop());
    }


    IEnumerator Enemyrop()
    {
        while(EnemyCount < 5)
        {
            xPos = Random.RandomRange(59, 99);
            zPos = Random.RandomRange(18, -16);

            Instantiate(Enemy[2], new Vector3(xPos, 0.5f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

            EnemyCount += 1;

        }

    }

    
}
