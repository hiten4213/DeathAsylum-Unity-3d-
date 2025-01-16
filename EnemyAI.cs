using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rangeofchase = 1f;
    [SerializeField] float turnspeed = 3f;
    [SerializeField] AudioClip zombieChaseSound; 
    [SerializeField] AudioClip zombieIdleSound;  

    private NavMeshAgent navmeshagent;
    private Animator animator;
    private AudioSource zombieAudioSource;
    private float distanceToTarget = Mathf.Infinity;
    //private bool isprovoked = false;
    private bool isChasing = false; 

    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        zombieAudioSource = GetComponent<AudioSource>();

        PlayIdleSound(); 
    }

    void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= rangeofchase)
        {
            
            EngageTarget();
        }
        else if (distanceToTarget > rangeofchase)
        {
            
            if (isChasing) 
            {
                StopChasing();
            }
        }
    }

    void EngageTarget()
    {
        facedirection();
        if (distanceToTarget >= navmeshagent.stoppingDistance)
        {
            Chase();
        }
        else if (distanceToTarget <= navmeshagent.stoppingDistance)
        {
            Attack();
        }
    }

    private void Chase()
    {
        if (!isChasing) 
        {
            StartChasing();
        }

        animator.SetBool("attack", false);
        animator.SetTrigger("Move");
        navmeshagent.SetDestination(target.position);
    }

    void Attack()
    {
        animator.SetBool("attack", true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeofchase);
    }

    void facedirection()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnspeed);
    }

    void StartChasing()
    {
        isChasing = true;
        PlayChaseSound();
    }

    void StopChasing()
    {
        isChasing = false;
        PlayIdleSound();
        animator.SetTrigger("idle");
    }

    void PlayChaseSound()
    {
        if (zombieChaseSound != null)
        {
            zombieAudioSource.clip = zombieChaseSound;
            zombieAudioSource.loop = true; // Loop the chase sound
            zombieAudioSource.Play();
        }
    }

    void PlayIdleSound()
    {
        if (zombieIdleSound != null)
        {
            zombieAudioSource.clip = zombieIdleSound;
            zombieAudioSource.loop = true; // Loop the idle sound
            zombieAudioSource.Play();
        }
    }
}
