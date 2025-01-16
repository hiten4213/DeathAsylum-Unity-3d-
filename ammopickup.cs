using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammopickup : MonoBehaviour
{
    [SerializeField] int AmmoAmount = 3;
    [SerializeField] ammotype ammotype;
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<ammo>().IncreaseCurrentAmmo(ammotype,AmmoAmount);
            Destroy(gameObject);
        }
    }
}
