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

    // Update is called once per frame
    void Update()
    {
        int previouslySelectedWeapon = _selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (_selectedWeapon >= transform.childCount - 1)
                _selectedWeapon = 0;
                    else
            _selectedWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (_selectedWeapon <= 0)
                _selectedWeapon = transform.childCount - 1;
            else
                _selectedWeapon--;
        }

        if (previouslySelectedWeapon != _selectedWeapon)
        {
            SelectWeapon();
        }
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
}
