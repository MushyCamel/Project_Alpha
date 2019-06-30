using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Properties")]
    public float gravity = -18f;
    public float h = 4f;
    //public float maxRange = 40f;
    public bool drawPath;
    public float delay = 3f;
    [Header("Explosion Properties")]
    public ForceMode _forceMode;
    public float _blastRadius = 20f;
    public float _force = 100f;
    public float _damage = 100f;

    int floorMask;
    float camRayLength = 100f;
    Rigidbody grenade;

    private float flightDuration;

    [Header("References")]
    public Transform Player;
    public Rigidbody grenadePrefab;
    public LayerMask clickMask;

    public void Initialize()
    {
        grenadePrefab = grenadePrefab.GetComponent<Rigidbody>();
    }

     public void Fire()
     {
        if (Input.GetButtonDown("Ability 1"))
        {
            Invoke("Explode", delay);
        }

        if (Input.GetButton("Ability 1"))
        {
            if (drawPath)
            {
                RenderThrowArc();
            }
        }

        if (Input.GetButtonUp("Ability 1"))
        {
            ThrowGrenade();
        }
     }

    void RenderThrowArc()
    {
        Vector3 MousePosition = -Vector3.one;
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));

        //Render the throwing arc  by using the data calculated.
        LaunchInfo launchInfo = CalculateLaunchInfo();
        Vector3 lastPoint = Player.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchInfo.timeToTarget;
            Vector3 displacement = launchInfo.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = Player.position + displacement;
            Debug.DrawLine(lastPoint, drawPoint, Color.red);
            lastPoint = drawPoint;
        }
    }

    public void ThrowGrenade()
    {
        Rigidbody grenade = Instantiate(grenadePrefab, Player.position, Player.rotation);

        Physics.gravity = Vector3.up * gravity;
        grenade.AddForceAtPosition(CalculateLaunchInfo().initialVelocity, Player.position, ForceMode.Impulse);

    }

    public void Explode()
    {
        GameObject grenadeThrown = GameObject.Find("Grenade(Clone)");
        if (grenadeThrown == null)
        {
            Collider[] collidersAtPlayer = Physics.OverlapSphere(transform.position, _blastRadius);

            foreach (Collider nearbyObject in collidersAtPlayer)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, nearbyObject.transform.position - transform.position, out hit, Mathf.Infinity))
                    {
                        if (hit.collider == nearbyObject)
                        {
                            rb.GetComponent<Rigidbody>().AddExplosionForce(_force, transform.position, _blastRadius, 0, _forceMode);
                            IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                            if (damagable != null)
                                damagable.TakeDamage(_damage);
                        }

                    }
                }
            }
            return;
        }
         
        Collider[] colliders = Physics.OverlapSphere(grenadeThrown.transform.position, _blastRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                RaycastHit hit;
                if (Physics.Raycast(grenadeThrown.transform.position, nearbyObject.transform.position - grenadeThrown.transform.position, out hit, Mathf.Infinity))
                {
                    if (hit.collider == nearbyObject)
                    {
                        rb.GetComponent<Rigidbody>().AddExplosionForce(_force, transform.position, _blastRadius, 0, _forceMode);
                                                    IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                        if (damagable != null)
                            damagable.TakeDamage(_damage);
                    }

                }
            }
        }

        Destroy(grenadeThrown);
    }

    struct LaunchInfo
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        //constructor
        public LaunchInfo(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

    LaunchInfo CalculateLaunchInfo()
    {

        Vector3 impactPosition = -Vector3.one;
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Create a RaycastHit variable to store information about what was hit by the ray.

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, camRayLength, clickMask))
        {
            impactPosition = hit.point;
        }

        float displacementY = impactPosition.y - Player.position.y;
        Vector3 displacementXZ = new Vector3(impactPosition.x - Player.position.x, 0, impactPosition.z - Player.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);

        // Calculate the velocity required to get to the 
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchInfo(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }
}

