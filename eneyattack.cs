using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eneyattack : MonoBehaviour
{
    playerhealth target;
    [SerializeField] float Enemydamage = 40f;
    void Start()
    {
        target = FindObjectOfType<playerhealth>();
    }
    public void hitEvent()
    {
        if(target == null) return;
        target.TakeDamage(Enemydamage);
        Debug.Log("ouch");
    }

}
