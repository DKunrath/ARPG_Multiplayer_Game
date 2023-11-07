using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class WeaponModelInstantiationSlot : MonoBehaviour
    {
        // What slot is this? (Left Hand or Right Hand, or Hips, or Back?)
        public WeaponModelSlot weaponSlot;
        public GameObject currentWeaponModel;

        public void UnloadWeapon()
        {
            if (currentWeaponModel != null)
            { 
                Destroy(currentWeaponModel);
            }
        }

        public void LoadWeapon(GameObject weaponModel)
        { 
            currentWeaponModel = weaponModel;
            weaponModel.transform.parent = transform;

            weaponModel.transform.localPosition = Vector3.zero;
            weaponModel.transform.localRotation = Quaternion.identity;
            weaponModel.transform.localScale = Vector3.one;
        }
    }
}