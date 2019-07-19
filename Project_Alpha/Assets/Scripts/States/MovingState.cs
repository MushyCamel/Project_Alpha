using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "States/MovingState")]
public class MovingState : BaseState
{

    public override async Task Enter(GameObject player)
    {
        await new WaitForEndOfFrame();
        Debug.Log("entered state");
    }

    public override async void Actions(GameObject player)
    {
        Debug.Log("Moving State");
        await new WaitForSeconds(2);
        Debug.Log("I waited for 2 seconds");
    }

    public override async Task Leave(GameObject player)
    {
        await new WaitForEndOfFrame();
        Debug.Log("leaving State");
    }
}
