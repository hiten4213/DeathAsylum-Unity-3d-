using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Torch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        torchclick();
    }
    void torchclick()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            GetComponentInChildren<Light>().enabled = true;
            GetComponentInParent<WeaponSwitcher>().enabled = false;

        }
        else{
            GetComponentInChildren<Light>().enabled = false;
            GetComponentInParent<WeaponSwitcher>().enabled = true;
        }
    }
}
