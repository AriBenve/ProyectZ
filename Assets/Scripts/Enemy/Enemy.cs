using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{

    public float _life;

    public float maxLife;

    Material _myMaterial;

    Color _initialColor;

    Coroutine _getHitFeedback_Cor;

    [SerializeField] GameObject _sandtornado;  
    

    private void Start()
    {

        _life = maxLife;

        _myMaterial = GetComponent<Renderer>().material;

        Instantiate(_sandtornado, transform.position, Quaternion.identity);

    }

    public void GetHit(float dmg)
    {
        _life -= dmg;
        
        if (_getHitFeedback_Cor != null)
        {
            StopCoroutine(_getHitFeedback_Cor);
            
        }

        if (_life <= 0)
        {
            ManagerEnemy.instance.Kill();
            
            Death();
        }
        else
        {
            
            _getHitFeedback_Cor = StartCoroutine(GetHitFeedback());
        }
    }

    void Death()
    {

        Instantiate(_sandtornado, transform.position, Quaternion.identity);

        StartCoroutine(ReduceToDeath());

    }

    IEnumerator GetHitFeedback()
    {
        float ticks = 0;

        float quantity = 2 / 0.1f;

        while (ticks < quantity)
        {
            _myMaterial.color = Color.red;

            yield return new WaitForSeconds(0.1f);

            _myMaterial.color = _initialColor;

            yield return new WaitForSeconds(0.1f);

            ticks++;
        }

        _getHitFeedback_Cor = null;
        
    }

    IEnumerator ReduceToDeath()
    {
        Vector3 initialScale = transform.localScale;

        float ticks = 0;

        _myMaterial.color = Color.red;

        while (ticks < 1)
        {
            ticks += Time.deltaTime;

            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, ticks);

            yield return null;
        }
        
        
        Destroy(this.gameObject);
    }
}
