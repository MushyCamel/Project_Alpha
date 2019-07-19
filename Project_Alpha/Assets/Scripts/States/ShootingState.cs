using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "States/ShootingState")]
public class ShootingState : BaseState
{
    private MachinegunScript machineGun;

    public override async Task Enter(GameObject player)
    {
        machineGun = player.GetComponentInChildren<MachinegunScript>();
        await new WaitForEndOfFrame();
        Debug.Log("entered state " + nameof(ShootingState));
    }

    public override async void Actions(GameObject player)
    {
        if (machineGun == null)
            return;
        machineGun.Fire();

    }

    public override async Task Leave(GameObject player)
    {
        await new WaitForEndOfFrame();
        Debug.Log("leaving State");

    }
}
