using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    [SerializeField]
    private float speed = 10;

    private Rigidbody rig;
    int floorMask;
    float camRayLength = 100f;

    // Start is called before the first frame update
    void Awake()
    {
        
        floorMask = LayerMask.GetMask("Floor");

        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;

        rig.MovePosition(transform.position + movement);

        Turning();
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rig.MoveRotation(newRotation);
        }
    }
}
