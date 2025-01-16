using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyhealth : MonoBehaviour
{
  Animator animator;
  [SerializeField] bool destroy = false;
  [SerializeField] float hitpoints = 100f; 
    public void TakeDamage(float damage)
    {
       hitpoints = hitpoints - damage;
       if(hitpoints <= 0 )
       {
        Debug.Log("Killed");
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        GetComponent<Animator>().SetTrigger("dead");
        GetComponent<EnemyAI>().enabled = false;
        if(destroy == true)
        {
          Invoke("destroyobj",1.5f);
        }

       }
    }
    void destroyobj()
    {
        Destroy(gameObject);
    }
}
