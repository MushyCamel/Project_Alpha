using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : GunBaseScript
{
    [SerializeField]
    private float _range = 100f;
    [SerializeField]
    private float _damage = 2f;
    [SerializeField]
    private float _fireRate = 5f;
    [SerializeField]
    private float _bulletsPerShot = 4;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if (_currentAmmo <= 0)
        {
            Invoke("Reload", reloadTime);
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / _fireRate;
            ShootShotgun();
        }
    }
    private void ShootShotgun()
    {
        _muzzle.Play();

        _currentAmmo--;

        Debug.Log(_currentAmmo);
        //moves shot within the 
        Vector3 direction = gun.transform.forward;

        //bullet spread for each of the rays that are cast
        for (int i = 1; i <= _bulletsPerShot; i++)
        {
            direction.x += UnityEngine.Random.Range(-_bulletSpread, _bulletSpread);
            direction.y += UnityEngine.Random.Range(-_bulletSpread, _bulletSpread);
            direction.z += UnityEngine.Random.Range(-_bulletSpread, _bulletSpread);

            RaycastHit hit;
            if (Physics.Raycast(gun.transform.position, direction, out hit, _range))
            {
                Debug.DrawRay(gun.transform.position, direction * _range, Color.red, 1f);
                Debug.Log(hit.transform.name);
                IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(_damage);

                }
            }

            //creates an impact effect at the hit location then destroys it
            if (hit.point != Vector3.zero)
            {
                GameObject impactGO = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }
        }

    }
}
