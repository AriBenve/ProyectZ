using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public Transform[] target;
    public float speed = 6.0f;

    int curPos = 0;
    int nextPos = 1;

    bool moveNext = true;
    public float timeToNext = 0.5f;

    private void FixedUpdate()
    {
        if (moveNext)
            transform.position = Vector3.MoveTowards(transform.position, target[nextPos].position, speed * Time.deltaTime);       //mueve la plataforma al siguiente punto

        if (Vector3.Distance(transform.position, target[nextPos].position) <= 0)
        {
            StartCoroutine(TimeMove());                                             //permite que la plataforma se quede quieta
            curPos = nextPos;                                                       //indica el siguiente punto
            nextPos++;                                                              //le suma uno al siguiente punto
                                                                                    
            if (nextPos > target.Length - 1)                                        //evita que la plataforma se vaya mas alla de los puntos pre seleccionados
                nextPos = 0;                                                        
        }
    }
    
    IEnumerator TimeMove()
    {
        moveNext = false;
        yield return new WaitForSeconds(timeToNext);
        moveNext = true;
    }
}
