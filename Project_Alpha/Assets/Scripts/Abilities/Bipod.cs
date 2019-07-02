using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bipod : MonoBehaviour
{
    [Header("Properties")]
    public float rMin = 315;
    public float rMax = 45;
    private float _viewAmount;
    public float lockedTime = 1f;

    [Header("References")]
    public Rigidbody player;

    private PlayerMovement playerMovement;
    private SniperScript sniper;



    void Update()
    {
        if (Input.GetButtonDown ("Ability 1"))
        {
            var angle = Mathf.Clamp(_viewAmount, rMax, rMin);
            Quaternion target = Quaternion.Euler(0, 0, angle); // any value as you see fit
            transform.rotation = target;
            /*
            var rMax = Quaternion.LookRotation(Vector3.forward);
            rMax *= Quaternion.Euler(0, viewAmount, 0);

            var rMin = Quaternion.LookRotation(Vector3.forward);
            rMin *= Quaternion.Euler(0, -viewAmount, 0);
            */
            //player.constraints = RigidbodyConstraints.FreezePosition;

            //if (Input.GetButtonDown("Ability 1"))
           // {
           //     player.constraints = ~RigidbodyConstraints.FreezePosition;
           // }

        }
    }
}
