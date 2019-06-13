using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBaseScript : MonoBehaviour
{
    private float _damage = 10f;
    [SerializeField]
    private float _bulletSpread = 0.02f;
    [SerializeField]
    private ParticleSystem _muzzle;
    [SerializeField]
    private GameObject _impactEffect;

    public GameObject gun;

    void Update()
    {
      /* if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        */
    }

    protected void Shoot(float _range)
    {
        _muzzle.Play();

        Vector3 direction = gun.transform.forward;
        direction.x += Random.Range(-_bulletSpread, _bulletSpread);
        direction.y += Random.Range(-_bulletSpread, _bulletSpread);
        direction.z += Random.Range(-_bulletSpread, _bulletSpread);

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
