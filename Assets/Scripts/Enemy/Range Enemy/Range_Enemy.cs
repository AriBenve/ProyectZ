using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_Enemy : Movent_Enemy
{

    bool alreadyAttack;

    protected override void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerMask);

        enemy.SetDestination(player.position);

        _anim.SetFloat("Speed", enemy.velocity.magnitude);

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            _anim.SetBool("Attacking", true);
        }
        else
        {
            _anim.SetBool("Attacking", false);
        }
    }


    private void AttackPlayer()
    {
        enemy.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttack)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);

            alreadyAttack = true;
            Invoke(nameof(ResetAtatck), timeBetweenAttacks);
        }
    }



    private void ResetAtatck()
    {
        alreadyAttack = false;
    }


}
