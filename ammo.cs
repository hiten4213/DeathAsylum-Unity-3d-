using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo : MonoBehaviour
{
  [SerializeField] ammoslot[] ammoslots;
    [System.Serializable]
    private class ammoslot
    {
        public ammotype AmmoType;
        public int AmmoCount;
    }

    public int getcurrentammo(ammotype ammotype)
    {
       return getammoslot(ammotype).AmmoCount;
    }
    public void ReduceCurrentAmmo(ammotype ammotype)
    {
      getammoslot(ammotype).AmmoCount--;
    }
    public void IncreaseCurrentAmmo(ammotype ammotype, int AmmoCount)
    {
      getammoslot(ammotype).AmmoCount+= AmmoCount;
    }
    private ammoslot getammoslot(ammotype ammotype)
    {
        foreach(ammoslot slot in ammoslots)
        {
          if(slot.AmmoType == ammotype)
          {
            return slot;
          }
        }
        return null;
    }
}
