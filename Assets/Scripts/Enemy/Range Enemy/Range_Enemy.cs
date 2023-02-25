using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_Enemy : Movent_Enemy
{

    bool alreadyAttack;
    public Transform Point; 

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
            Get_Fire_Ball();

            alreadyAttack = true;
            
            Invoke(nameof(ResetAtatck), timeBetweenAttacks);
        }
    }
    public GameObject Get_Fire_Ball()
    {

        GameObject obj = Instantiate(projectile, Point.transform.position, Point.transform.rotation) as GameObject;
        obj.GetComponent<Rigidbody>().AddForce(transform.forward * 64f, ForceMode.Impulse);

        return obj;
    }


    private void ResetAtatck()
    {
        alreadyAttack = false;
    }


}
