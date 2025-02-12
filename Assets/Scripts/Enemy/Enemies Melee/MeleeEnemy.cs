using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Movent_Enemy
{
    [SerializeField] GameObject[] hitbox;
    int hit_Select;


    protected override void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerMask);

        enemy.SetDestination(player.position);

        _anim.SetFloat("Speed", enemy.velocity.magnitude);

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);

        _anim.SetBool("Attacking", playerInSightRange && playerInAttackRange);

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
        hitbox[hit_Select].GetComponent<CapsuleCollider>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        hitbox[hit_Select].GetComponent<CapsuleCollider>().enabled = false;
    }


}
