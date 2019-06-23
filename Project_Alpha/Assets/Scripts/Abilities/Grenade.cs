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
    int floorMask;
    float camRayLength = 100f;

    private float flightDuration;

    [Header("References")]
    public Transform Player;
    public Rigidbody grenadePrefab;
    public LayerMask clickMask;

    void Start()
    {
        grenadePrefab = grenadePrefab.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Ability 1"))
        {
            //spawn grenade, spawn countdown
        }
        if (Input.GetButton("Ability 1"))
        {
            if (drawPath)
            {
                RenderThrowArc();
                //GrenadeExplosion();
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

    void ThrowGrenade()
    {
        Rigidbody grenade = Instantiate(grenadePrefab, Player.position, Player.rotation);

        Physics.gravity = Vector3.up * gravity;
        grenade.AddForceAtPosition(CalculateLaunchInfo().initialVelocity, Player.position, ForceMode.Impulse);
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

