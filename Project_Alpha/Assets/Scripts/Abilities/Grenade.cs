using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public float throwRange = 40f;
    int floorMask;
    float camRayLength = 100f;


    private float flightDuration;

    public GameObject prefab;

    public void Update()
    {
        if (Input.GetButtonDown("Ability1"))
        {
           
            ThrowGrenade();
        }
    }


    public void ThrowGrenade()
    {
        Instantiate(prefab, Target.position, Target.rotation);

        Transform Projectile = prefab.transform;

        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        Physics.Raycast(camRay, out floorHit, camRayLength, floorMask);
        
            Vector3 grenadeToMouse = floorHit.point - transform.position;

            // Calculate distance to target
            float target_Distance = Vector3.Distance(Projectile.position, Target.position);

            if (target_Distance < throwRange)
            {
                // Move projectile to the position of throwing object + add some offset if needed.
                Projectile.position = grenadeToMouse; //+ new Vector3(0, 0.0f, 0);


                // Calculate the velocity needed to throw the object to the target at specified angle.
                float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

                // Extract the X  Y componenent of the velocity
                float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
                float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

                // Calculate flight time.
                float flightDuration = target_Distance / Vx;

                // Rotate projectile to face the target.
                Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

                float elapse_time = 0;

                while (elapse_time < flightDuration)
                {
                    Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

                    elapse_time += Time.deltaTime;
                }
            }
        }
    }

