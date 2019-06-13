using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : GunBaseScript
{
    [SerializeField]
    private float _damage = 10f;
    [SerializeField]
    private float _range = 100f;
    [SerializeField]
    private float _fireRate = 5f;

    public float reloadTime;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot(_range);
        }
    }
}
