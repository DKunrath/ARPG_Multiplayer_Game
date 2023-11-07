using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DK
{
    public class WorldItemDatabase : MonoBehaviour
    {
        public static WorldItemDatabase Instance;

        public WeaponItem unarmedWeapon;

        // A list of every weapon we have in the game
        [Header("Weapons")]
        [SerializeField] List<WeaponItem> weaponsInTheGame = new List<WeaponItem>();

        // A list of every item we have in the game
        [Header("Items")]
        [SerializeField] List<Item> itemsInTheGame = new List<Item>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // Add all of our weapons to the list of items
            foreach (var weapon in weaponsInTheGame)
            {
                itemsInTheGame.Add(weapon);
            }

            // Assign all of our items with an item ID
            for (int i = 0; i < itemsInTheGame.Count; i++)
            {
                itemsInTheGame[i].itemID = i;
            }
        }

        public WeaponItem GetWeaponByID(int ID)
        {
            return weaponsInTheGame.FirstOrDefault(weapon => weapon.itemID == ID);
        }
    }
}