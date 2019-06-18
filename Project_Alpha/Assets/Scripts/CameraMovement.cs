using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _cameraSpeed = 15;
    public bool smoothFollow = true;
    public GameObject player;
    public float offsetX = -5;
    public float offsetZ = 0;

   //The maximum distance permited to the camera to be far from the player, its used to make a smooth movement 
    public float maximumDistance = 2;
    //The velocity of your player, used to determine que speed of the camera 
    public float playerVelocity = 10;

    private float _movementX;
    private float _movementZ;



    void Update()
    {
     FollowPlayer();


    }

    private void FollowPlayer()
    {
        Vector3 newPos = transform.position;
        _movementX = ((player.transform.position.x + offsetX - this.transform.position.x)) / maximumDistance;
        _movementZ = ((player.transform.position.z + offsetZ - this.transform.position.z)) / maximumDistance;
        this.transform.position += new Vector3((_movementX * playerVelocity * Time.deltaTime), 0, (_movementZ * playerVelocity * Time.deltaTime));


        if (!smoothFollow)
        {
            transform.position = newPos;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, newPos, _cameraSpeed * Time.deltaTime);
        }
    }
}

