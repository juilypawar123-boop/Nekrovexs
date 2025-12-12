using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiAi : MonoBehaviour
{
    NavMeshAgent _agent;
    Animator _animator;

    public GameObject _Target;
    public float detectionRadius = 10f;   // Distance to start chasing player
    public float attackDistance = 7f;     // Distance to attack
    public float attackAngle = 700f;       // Angle in front of player to attack

    // Wandering variables
    public float wanderRadius = 3f;       // Small radius to wander around spawn
    public float wanderSpeed = 1.5f;      // Speed while wandering
    private Vector3 spawnPoint;           // Zombie spawn position
    private bool isWandering = false;
    private Vector3 wanderTarget;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _Target = GameObject.FindGameObjectWithTag("Player");

        spawnPoint = transform.position;  // Save the initial spawn point
        _agent.speed = wanderSpeed;       // Default speed for wandering

        // Force snap to NavMesh before doing anything
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            _agent.Warp(hit.position);  // Force the agent to register on the NavMesh
        }

    }


    void Update()
    {
        if (_Target == null || _agent == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _Target.transform.position);

        // Calculate direction and angle early
        Vector3 directionToPlayer = (_Target.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (distanceToPlayer <= detectionRadius)
        {
            // Wandering logic...

            // Attack check
            if (distanceToPlayer <= attackDistance && angle <= attackAngle)
            {
                _agent.isStopped = true;
                _animator.SetBool("walk", false);

                // Smooth rotation towards player
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                _animator.SetTrigger("attack");
            }
            else
            {
                _agent.isStopped = false;
            }

            // Pick new wander target if reached current one
            if (Vector3.Distance(transform.position, wanderTarget) < 0.5f)
            {
                PickNewWanderTarget();
            }
        }
        else
        {
            // Player is far: stay idle
            _agent.SetDestination(transform.position);
            _animator.SetBool("walk", false);
            isWandering = false;
        }
    }

    void PickNewWanderTarget()
    {
        // Pick a random point within a small radius around spawn
        Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
        wanderTarget = spawnPoint + new Vector3(randomOffset.x, 0, randomOffset.y);
    }
}

