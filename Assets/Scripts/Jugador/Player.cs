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

    [SerializeField]BloodEffect _myBloodEffect;

    private void Start()
    {
        _life = maxLife;
    }

    private void Update()
    {
        if (_life > maxLife && !running)
        {
            running = true;
            StartCoroutine(GraduallyReduceHP(_life - maxLife, 5f));
        }
    }

    public void Damage(float d)
    {

        _life -= d;
        Debug.Log(_life);

        if (_myBloodEffect == null)
        {
            _myBloodEffect = FindObjectOfType<BloodEffect>();
        }
        _myBloodEffect.UpdateLifeView(_life / maxLife);

        if (_life <= 0)
        {
            Death();
        }
    }

    public void Heal(float amount)
    {
        _life += amount;
    }

    public IEnumerator GraduallyReduceHP(float damage, float rate)
    {
        while (damage > 0)
        {
            float delta = rate * Time.deltaTime;
            if(_life <= 0)
            {
                Death();
            }
            if(delta > damage)
            {
                _life -= damage;
                break;
            }
            _life -= delta;
            damage -= delta;
            yield return null;
        }

        running = false;
    }

    public void Death()
    {
        Debug.Log("Me fulminaron wachin que carajo hiciste pedazo de aweonado");
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene("Nivel 1 (Re-Design)");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
