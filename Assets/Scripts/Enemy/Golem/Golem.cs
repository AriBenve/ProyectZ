using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Movent_Enemy
{
    [SerializeField] float Speed;
    /*[SerializeField] int ChargerDistance;*/
    [SerializeField] float WalkDistance;
    [SerializeField] int timer;
    float Distance;
    
    
    [SerializeField] GameObject[] hitbox;
    int hit_Select;

    protected override void Update()
    {
        Distance = Vector3.Distance(player.position, this.transform.position);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerMask);

        enemy.SetDestination(player.position);

        _anim.SetFloat("Speed", enemy.velocity.magnitude);

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);

        _anim.SetBool("Attacking", playerInSightRange && playerInAttackRange);

        if (Distance > WalkDistance)
        {
            enemy.speed = 30;
            _anim.SetBool("Charging", true);
            
        }
        else
        {
            enemy.speed = 10;
            _anim.SetBool("Charging", false);
        }


        if (playerInSightRange && playerInAttackRange)
        {
            _anim.SetBool("Attacking", true);
        }
        else
        {
            _anim.SetBool("Attacking", false);
        }
    }

    public void ColliderWeaponTrue()
    {
        hitbox[hit_Select].GetComponent<Collider>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        hitbox[hit_Select].GetComponent<Collider>().enabled = false;
    }
}
