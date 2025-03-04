using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentweapon = 0;

    void Start()
    {
        SetWeaponActive();
    }

    
    void Update()
    {
        int previousweapon = currentweapon;
        ProcessKeyInput();
        ProcessScrollWheel();
        if(previousweapon != currentweapon)
        {
            SetWeaponActive();
        }
    }
    void SetWeaponActive()
    {
        int weaponindex = 0;
        foreach (Transform weapon in transform)
        {
            if(weaponindex == currentweapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponindex++;
        }

    }
    public void ProcessKeyInput()
    {
       if(Input.GetKeyDown(KeyCode.Alpha1))
       {
        currentweapon = 0;
       }
       if(Input.GetKeyDown(KeyCode.Alpha2))
       {
        currentweapon = 1;
       }
       if(Input.GetKeyDown(KeyCode.Alpha3))
       {
        currentweapon = 2;
       }
    }
    void ProcessScrollWheel()
    {
       if(Input.GetAxis("Mouse ScrollWheel")>0)
       {
          if(currentweapon >= transform.childCount-1)
          {
            currentweapon = 0;
          }
          else
          {
            currentweapon++;
          }
       }
       if(Input.GetAxis("Mouse ScrollWheel")<0)
       {
          if(currentweapon <= 0)
          {
            currentweapon = transform.childCount-1;
          }
          else
          {
            currentweapon--;
          }
       }
    }
}
