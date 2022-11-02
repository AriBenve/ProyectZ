using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TEMPORAL : MonoBehaviour
{


    public NavMeshAgent enemy;
    public Transform player;
    public GameObject projectile;
    

    public LayerMask FloorMask, PlayerMask;

    public float timeBetweenAttacks;
    bool alreadyAttack;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerMask);

        enemy.SetDestination(player.position);

        

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
