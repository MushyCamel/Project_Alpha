using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading;

[CreateAssetMenu(menuName = "States/MovingState")]
public class MovingState : BaseState
{
    public IdleState idleState;
    public ShootingState shootingState;

    public float speed = 10;

    private Rigidbody rig;
    private Transform playerTransform;
    int floorMask;
    float camRayLength = 100f;

    CancellationTokenSource idleStateCheckToken;

    public override async Task Enter(GameObject player)
    {
        floorMask = LayerMask.GetMask("Floor");

        rig = player.GetComponent<Rigidbody>();

        playerTransform = player.transform;

        idleStateCheckToken = new CancellationTokenSource();
        IdleStateCheck(idleStateCheckToken.Token);

        await new WaitForEndOfFrame();
        Debug.Log("entered state " + nameof(MovingState));
    }

    public override async void Actions(GameObject player)
    { 
        Move();
        Turning();

        if (Input.GetButtonDown("Fire1"))
        {
            TriggerState(shootingState);
        }
    }

    public override async Task Leave(GameObject player)
    {
        //cancels token to stop potential while true look continuing to loop indefinately
        idleStateCheckToken.Cancel();
        await new WaitForEndOfFrame();
        Debug.Log("leaving State");

    }

    public void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;

        rig.MovePosition(rig.transform.position + movement);
    }

    public void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - rig.transform.position;

            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rig.MoveRotation(newRotation);
        }
    }

    public async void IdleStateCheck(CancellationToken token)
    {
        //Checks if the player position is the same after a second and triggers idle
        while (token.IsCancellationRequested == false)
        {
            var currentPosition = playerTransform.position;
            var currentRotation = playerTransform.rotation;
            await new WaitForSeconds(1);
            if(playerTransform.position == currentPosition && playerTransform.rotation == currentRotation)
            {
                TriggerState(idleState);
                Stop();                
            }
        }
    }

}
