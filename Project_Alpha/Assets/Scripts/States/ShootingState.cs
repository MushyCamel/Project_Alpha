using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "States/ShootingState")]
public class ShootingState : BaseState
{
    public enum GunType
    {
        Primary = 0,
        Secondary = 1,
    }

    public static GunType currentGun = GunType.Primary;

    private GunBaseScript primaryGun;
    private GunBaseScript secondaryGun;

    public MovingState movingState;

    public override async Task Enter(GameObject player)
    {
        if (primaryGun == null)
            primaryGun = player.GetComponentInChildren<MachinegunScript>();
        if (primaryGun == null)
            primaryGun = player.GetComponentInChildren<SniperScript>();
        if (primaryGun == null)
            primaryGun = player.GetComponentInChildren<ShotgunScript>();

        if(secondaryGun == null)
            secondaryGun = player.GetComponentInChildren<PistolScript>();

        await new WaitForEndOfFrame();
        Debug.Log("entered state " + nameof(ShootingState));
    }

    public override void Actions(GameObject player)
    {
        if(currentGun == GunType.Primary)
        {
            primaryGun.Fire();
        }
        else if(currentGun == GunType.Secondary)
        {
            secondaryGun.Fire();
        }
    }

    public override async Task Leave(GameObject player)
    {
        await new WaitForEndOfFrame();
        Debug.Log("leaving State");
    }
}
