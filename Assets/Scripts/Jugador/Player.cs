using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour,Idamage
{
    [Header("Floats")]
    public float _life;
    public float maxLife;

    [Header("Bools")]
    private bool running;


    public Image bloodEffectImage;

    private float a;

    private void Start()
    {
        _life = maxLife;

        a = bloodEffectImage.color.a;
    }

    //private void Update()
    //{
    //    if(_life > maxLife)
    //    {
    //        float amount = _life - maxLife;
    //        StartCoroutine(GraduallyReduceHP(amount, 5f));
    //    }
    //}

    public void Damage(float d)
    {
        _life -= d;
        Debug.Log(_life);

        if (_life == d)
        {
            a += 0.01f;
        }
        a -= 0.001f;

        a = Mathf.Clamp(a, 0, 1f);

        if (_life <= 0)
        {
            Death();
        }
    }

    public void Heal(float amount)
    {
        _life += amount;
    }

    //IEnumerator GraduallyReduceHP(float damage, float rate)
    //{
    //    while (damage > 0)
    //    {
    //        float delta = rate * Time.deltaTime;

    //        if(delta > damage)
    //        {
    //            _life -= damage;
    //            break;
    //        }
    //        _life -= delta;
    //        damage -= delta;
    //        yield return null;
    //    }
    //}

    public void Death()
    {
        Debug.Log("Me fulminaron wachin que carajo hiciste pedazo de aweonado");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
