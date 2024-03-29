﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "States/IdleState")]
public class IdleState : BaseState
{
    public MovingState movingState;

    //When entering the state
    public override async Task Enter(GameObject player)
    {

        await new WaitForEndOfFrame();
        Debug.Log("entered state " + nameof(IdleState));
    }

    //while in the state
    public override void Actions(GameObject player)
    {
        if(Input.anyKeyDown || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
           TriggerState(movingState);
        }
    }

    //when leaving the state
    public override async Task Leave(GameObject player)
    {

        await new WaitForEndOfFrame();
        Debug.Log("leaving State");

    }
}
