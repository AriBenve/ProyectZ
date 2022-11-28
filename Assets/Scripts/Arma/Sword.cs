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
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null && swordAmmo <= 0)
        {
            if (enemy._life <= dmg)
            {
                if(enemy._life <= 100)
                {
                    swordAmmo--;
                }
                else if(enemy._life >= 101 && enemy._life <= 200)
                {
                    swordAmmo -= maxSwordAmmo - 1;
                }
                else
                {
                    swordAmmo -= maxSwordAmmo;
                }
                //Curaciones y daño al enemigo
                FindObjectOfType<Player>().Heal(Random.Range(50, enemy.maxLife));
                enemy.GetHit(dmg);
            }
            else 
            {
                enemy.GetHit(dmg);
                swordAmmo -= maxSwordAmmo;
            }
                
        }


                
        else
            Debug.Log("Weon reculeado no puedo suicidarme ||todavia|| tengo un egipto entero que rescatar");
    }
}
