using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] float dmg;

    [SerializeField] int maxSwordAmmo;

    public int swordAmmo;

    public KeyCode attackKey;

    public Animator _animator;

    private void Start()
    {
        swordAmmo = maxSwordAmmo;
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if(Input.GetKeyDown(attackKey))
        {
            _animator.SetInteger("Random", Random.Range(1, 4));
            _animator.SetTrigger("ataque");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy _enemy = other.GetComponent<Enemy>();

        Debug.Log(other);

        if (_enemy != null && swordAmmo >= 0)
        {
            if (_enemy._life <= dmg)
            {
                if(_enemy._life <= 100)
                {
                    swordAmmo--;
                }
                else if(_enemy._life >= 101 && _enemy._life <= 200)
                {
                    swordAmmo -= maxSwordAmmo - 1;
                }
                else
                {
                    swordAmmo -= maxSwordAmmo;
                }
                //Curaciones y daño al enemigo
                FindObjectOfType<Player>().Heal(Random.Range(50, _enemy.maxLife));
                _enemy.GetHit(dmg);
            }
            else 
            {
                _enemy.GetHit(dmg);
                swordAmmo -= maxSwordAmmo;
            }
                
        }                
        else
            Debug.Log("Weon reculeado no puedo suicidarme ||todavia|| tengo un egipto entero que rescatar");
    }
}
