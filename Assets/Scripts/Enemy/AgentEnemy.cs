using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public GameObject projectile;
    private Animator _evokerAnimator;

    public LayerMask FloorMask, PlayerMask;

    public float timeBetweenAttacks;
    bool alreadyAttack;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
        _evokerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerMask);
        
        enemy.SetDestination(player.position);

        _evokerAnimator.SetFloat("Speed", enemy.velocity.magnitude);
        
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            _evokerAnimator.SetBool("Attacking", true);
        }
        else
        {
            _evokerAnimator.SetBool("Attacking", false);
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
