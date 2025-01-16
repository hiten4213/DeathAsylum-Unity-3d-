using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    weapons weapon;

   [SerializeField] Cinemachine.CinemachineVirtualCamera cameraa;
   [SerializeField] int GunZoom = 1;
    void Update()
    {
        weaponzooming();
    }
    void weaponzooming()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Zoomin();
            GetComponentInParent<WeaponSwitcher>().enabled = false;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            Zoomout();
            GetComponentInParent<WeaponSwitcher>().enabled = true;
        }
    }
    void Zoomout()
    {
       cameraa.m_Lens.FieldOfView += GunZoom;
    }
    void Zoomin()
    {
       cameraa.m_Lens.FieldOfView -= GunZoom;
    }
}
