using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField]
    private int _selectedWeapon = 0;
    [SerializeField]
    private float _switchingTime = .2f;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == _selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    public void Switch()
    {
        if (_selectedWeapon >= transform.childCount - 1)
            _selectedWeapon = 0;
        else
            _selectedWeapon++;

        SelectWeapon();
    }
}
