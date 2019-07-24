using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int _maxHealth = 10;
    [SerializeField]
    private float _blastRadius = 5f;
    [SerializeField]
    private float _force = 700f;
    [SerializeField]
    private ForceMode _forceMode;

    private float _health;
    private PlayerStats playerStats;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        _health -= _damage;
        if (_health <= 0)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            Debug.Log("BOOM!");
            Explode();
            playerStats.kills++;
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _blastRadius);

        foreach (Collider nearbyObject in colliders)
        {
           Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, nearbyObject.transform.position - transform.position, out hit, Mathf.Infinity))
                {
                    if (hit.collider == nearbyObject)
                    {
                        rb.GetComponent<Rigidbody>().AddExplosionForce(_force, transform.position, _blastRadius, 0, _forceMode);
                    }

                }
            }
        }
    }
}
