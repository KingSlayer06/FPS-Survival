using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Range(0f,5f)][SerializeField]private int weaponIndex;

    [SerializeField] private WeaponHandler[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        weaponIndex = 0;
        weapons[weaponIndex].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeaponIndex = weaponIndex;

        // Scroll Wheel Up
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponIndex >= weapons.Length - 1)
                weaponIndex = 0;
            else
                weaponIndex++;
        }

        // Scroll Wheel Down
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponIndex <= 0)
                weaponIndex = weapons.Length - 1;
            else
                weaponIndex--;
        }

        // Num Keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) weaponIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) weaponIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) weaponIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) weaponIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) weaponIndex = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6)) weaponIndex = 5;

        // Call the Fn if weapon changed
        if (previousWeaponIndex != weaponIndex)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (WeaponHandler weapon in weapons)
        {
            if (i == weaponIndex)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    public WeaponHandler GetActiveWeapon()
    {
        return weapons[weaponIndex];
    }
}
