using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCycler : MonoBehaviour
{
    public int weaponIndex;
    public WeaponCollection weaponCollection;

    private PlayerAttacks playerAttacks;
    // Start is called before the first frame update
    void Start()
    {
        playerAttacks = GetComponent<PlayerAttacks>();
        playerAttacks.SetWeapon(weaponCollection.weapons[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            CycleWeapon();
    }
    void CycleWeapon()
    {
        weaponIndex++;
        if (weaponIndex >= weaponCollection.weapons.Length)
            weaponIndex = 0;
        playerAttacks.SetWeapon(weaponCollection.weapons[weaponIndex]);
    }
}
