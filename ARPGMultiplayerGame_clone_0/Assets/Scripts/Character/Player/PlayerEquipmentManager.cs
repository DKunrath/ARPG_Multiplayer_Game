using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace DK
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        PlayerManager player;

        public WeaponModelInstantiationSlot rightHandSlot;
        public WeaponModelInstantiationSlot leftHandSlot;

        [SerializeField] WeaponManager rightWeaponManager;
        [SerializeField] WeaponManager leftWeaponManager;

        public GameObject rightHandWeaponModel;
        public GameObject leftHandWeaponModel;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();

            // Get our slots
            InitializeWeaponSlots();
        }

        protected override void Start()
        {
            base.Start();

            LoadWeaponsOnBothHands();
        }

        private void InitializeWeaponSlots()
        {
            WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();

            foreach (var weaponSlot in weaponSlots)
            {
                if (weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
                {
                    Debug.Log(weaponSlot.weaponSlot);
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHand)
                {
                    Debug.Log(weaponSlot.weaponSlot);
                    leftHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponsOnBothHands()
        {
            LoadRightWeapon();
            LoadLeftWeapon();
        }

        // RIGHT WEAPON

        public void LoadRightWeapon()
        {
            if (player.playerInventoryManager.currentRightHandWeapon != null)
            {
                // Remove the old weapon
                rightHandSlot.UnloadWeapon();

                // Bring in the new weapon
                rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
                rightHandSlot.LoadWeapon(rightHandWeaponModel);
                rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
                // Assign weapons damage, to its collider
                rightWeaponManager.SetWeaponDamage(player ,player.playerInventoryManager.currentRightHandWeapon);
            }
        }

        public void SwitchRightWeapon()
        {
            if (!player.IsOwner) return;

            // Animation for swapping weapons on the right hand
            player.playerAnimatorManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, true, true, true);

            // ELDEN RINGS WEAPON SWAPPING
            // 1. Check if we have another weapon besides our main weapon, if we do, NEVER swap to unarmed, rotate between weapon 1 and 2
            // 2. If we dont, swap to unarmed, then SKIP the other empty slot and swap back. Do not process both empty slots before returning to main weapon

            WeaponItem selectedWeapon = null;

            // Disable two handing if we are two handing

            // Add one to our index to switch to the next potential weapon
            player.playerInventoryManager.rightHandWeaponIndex += 1;

            // If our index is out of bounds, reset it to position #1 (0)
            if (player.playerInventoryManager.rightHandWeaponIndex < 0 || player.playerInventoryManager.rightHandWeaponIndex > 2)
            { 
                player.playerInventoryManager.rightHandWeaponIndex = 0;

                // We check if we are holding more than one weapon
                float weaponCount = 0;
                WeaponItem firstWeapon = null;
                int firstWeaponIndex = 0;

                for (int i = 0; i < player.playerInventoryManager.weaponsInRightHandSlots.Length; i++)
                {
                    if (player.playerInventoryManager.weaponsInRightHandSlots[i].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        weaponCount += 1;

                        if (firstWeapon == null)
                        {
                            firstWeapon = player.playerInventoryManager.weaponsInRightHandSlots[i];
                            firstWeaponIndex = i;
                        }
                    }
                }

                if (weaponCount <= 1)
                {
                    player.playerInventoryManager.rightHandWeaponIndex = -1;
                    selectedWeapon = WorldItemDatabase.Instance.unarmedWeapon;
                    player.playerNetworkManager.currentRightHandWeaponID.Value = selectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.rightHandWeaponIndex = firstWeaponIndex;
                    player.playerNetworkManager.currentRightHandWeaponID.Value = firstWeapon.itemID;
                }

                return;
            }

            foreach (WeaponItem weapon in player.playerInventoryManager.weaponsInRightHandSlots)
            {
                // Check to see if this weapon is not the "UNARMED" weapon
                if (player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex];
                    // Assign the network weapon id so it switches for all connected clients
                    player.playerNetworkManager.currentRightHandWeaponID.Value = player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID;
                    return;
                }
            }

            if (selectedWeapon == null && player.playerInventoryManager.rightHandWeaponIndex <= 2)
            {
                SwitchRightWeapon();
            }
        }

        // LEFT WEAPON

        public void LoadLeftWeapon()
        {
            if (player.playerInventoryManager.currentLeftHandWeapon != null)
            {
                // Remove the old weapon
                leftHandSlot.UnloadWeapon();

                // Bring in the new weapon
                leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);
                leftHandSlot.LoadWeapon(leftHandWeaponModel);
                leftWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
                // Assign weapons damage, to its collider
                leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
            }
        }

        public void SwitchLeftWeapon()
        {
            //FAZER TUDO PARA A MAO ESQUERDA NAO ESQUECER
        }
    }
}