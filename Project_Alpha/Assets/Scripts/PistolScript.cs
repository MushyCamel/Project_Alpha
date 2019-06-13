using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : GunBaseScript
{
    [SerializeField]
    private float _damage = 10f;
    [SerializeField]
    private float _range = 100f;
    [SerializeField]
    private float _fireRate = 5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(_range);
        }
    }
}
