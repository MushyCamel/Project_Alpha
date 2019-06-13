using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBaseScript : MonoBehaviour
{
    //private float _damage = 10f;
    [SerializeField]
    private float _bulletSpread = 0.02f;
    [SerializeField]
    private ParticleSystem _muzzle;
    [SerializeField]
    private GameObject _impactEffect;
    [SerializeField]
    protected float reloadTime;
    [SerializeField]
    protected bool isReloading = false;

    public GameObject gun;

    protected int _currentAmmo;

    [SerializeField]
    protected int _maxAmmo;

    void Start()
    {
        _currentAmmo = _maxAmmo;
        Debug.Log(_currentAmmo);
    }


    protected void Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");
        _currentAmmo = _maxAmmo;
        isReloading = false;
    }

    protected void Shoot(float _range)
    {
        _muzzle.Play();

        _currentAmmo--;
        Debug.Log(_currentAmmo);

        //moves shot within the 
        Vector3 direction = gun.transform.forward;
        direction.x += UnityEngine.Random.Range(-_bulletSpread, _bulletSpread);
        direction.y += UnityEngine.Random.Range(-_bulletSpread, _bulletSpread);
        direction.z += UnityEngine.Random.Range(-_bulletSpread, _bulletSpread);

        //determins if the raycast hits an object and names it
        RaycastHit hit;
        if (Physics.Raycast(gun.transform.position, direction, out hit, _range))
        {
            Debug.Log(hit.transform.name);
        }

        //creates an impact effect at the hit location then destroys it
        if(hit.point != Vector3.zero)
        {
            GameObject impactGO = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }


    }

}
