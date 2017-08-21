using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Shooting : MonoBehaviour
    {
        public int weaponIndex = 0;

        private Weapon[] attachedWeapons;
        private Rigidbody2D rigid;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        void Start()
        {
            // Get all attachedWeapons in children
            attachedWeapons = GetComponentsInChildren<Weapon>();
            // Set the first weapon
            SwitchWeapon(weaponIndex);
        }

        // Update is called once per frame
        void Update()
        {
            CheckFire();
            WeaponSwitching();
        }

        // Checks if the user pressed a button to fire the current weapon
        void CheckFire()
        {
            // Set currentWeapon to attachedWeapons[weaponIdex]
            Weapon currentWeapon = attachedWeapons[weaponIndex];
            // IF user pressed down space
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // Fire currentWeapon
                currentWeapon.Fire();
                // Add recoil to player from weapon's recoil
                Vector3 force = -transform.right * currentWeapon.recoil;
                rigid.AddForce(force, ForceMode2D.Impulse);
            }
        }

        // Handles weapon switching when pressing keys
        void WeaponSwitching()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                CycleWeapon(-1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                CycleWeapon(1);
            }
        }

        // Cycles through the weapons using amount as an index
        void CycleWeapon(int amount)
        {
            // SET desiredIndex to weaponIndex + amount
            int desiredIndex = weaponIndex + amount;
            // IF desired index > length of weapons
            if (desiredIndex >= attachedWeapons.Length)
            {
                // Set desiredIndex to zero
                desiredIndex = 0;
            }
            // ELSE IF desiredIndex < zero
            else if(desiredIndex < 0)
            {
                // SET desiredIndex to length of weapons - 1
                desiredIndex = attachedWeapons.Length - 1;
            }
            // SET weaponIndex to desiredIdex
            weaponIndex = desiredIndex;
            // SwitchWeapon() to weaponIndex
            SwitchWeapon(weaponIndex);
        }
        // Disables all other weapons in the list and return the selected one
        Weapon SwitchWeapon(int weaponIndex)
        {
            // Check if index is outside of bounds
            if(weaponIndex < 0 || weaponIndex > attachedWeapons.Length)
            {
                // Return null weapon as error
                return null;
            }
            // Looping through all the weapon
            for (int i = 0; i < attachedWeapons.Length; i++)
            {
                // Get the weapon at i
                Weapon w = attachedWeapons[i];
                // IF i == weaponIndex
                if (i == weaponIndex)
                {
                    // Activate the weapon
                    w.gameObject.SetActive(true);
                }
                // ELSE
                else
                {
                    // Deactivate the weapon
                    w.gameObject.SetActive(false);
                }
            }
            // Return selected weapon
            return attachedWeapons[weaponIndex];
        }
    }
}
