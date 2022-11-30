using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    public static Enemy enemy;

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
        
      

        if (_life <= 0)
        {
            //ManagerEnemy.instance.Kill();
            
            Death();
        }
        
    }

    void Death()
    {

        ManagerEnemy.instance.Kill();
        Instantiate(_sandtornado, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        //StartCoroutine(ReduceToDeath());

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

        
       
    }
}
