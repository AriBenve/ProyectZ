using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour,Idamage
{
    public float life;

    private void Start()
    {
        life = 100;
    }

    private void Update()
    {
        if(life <= 0)
        {
            Death();
        }
    }

    public void Damage(float d)
    {
        life -= d;
        Debug.Log(life);
    }

    public void Death()
    {
        Debug.Log("Me fulminaron wachin que carajo hiciste pedazo de aweonado");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
