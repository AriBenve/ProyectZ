using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{

    public NavMeshAgent enemy;
    public Transform player;
   

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

        
    }


   

}
