using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ModeAttack : MonoBehaviour
{
    public GameObject projectile;
    public NavMeshAgent enemy;
    public Transform player;

    public float timeBetweenAttacks;
    bool alreadyAttack;
    public LayerMask FloorMask, PlayerMask;
    
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerMask);

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
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
