using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "States/BackgroundState")]
public class BackgroundState : BaseState
{
    private int timeToSwitch = 1;

    private WeaponSwitching weaponSwitching;

    private bool isSwitching;

    //When entering the state
    public override async Task Enter(GameObject player)
    {
        weaponSwitching = player.GetComponentInChildren<WeaponSwitching>();
        //var bob = PlayerStateController.FindState<MovingState>();
        //if (bob != null)
        //{
        //    bob.speed = 8; 
        //    timeToSwitch = 2;
        //}
        await new WaitForEndOfFrame();
        Debug.Log("entered state " + nameof(BackgroundState));
    }

    //while in the state
    public override void Actions(GameObject player)
    {
        WeaponSwitching();
    }

    //when leaving the state
    public override async Task Leave(GameObject player)
    {
        await new WaitForEndOfFrame();
        Debug.Log("leaving State");
    }

    private async void WeaponSwitching()
    {
        if (isSwitching)
        {
            var shootingState = PlayerStateController.FindState<ShootingState>();
            shootingState?.Stop();
            return;
        }

        if (Input.GetAxis("Mouse ScrollWheel") == 0)
            return;

        isSwitching = true;

        await new WaitForSeconds(timeToSwitch);

        if (ShootingState.currentGun == ShootingState.GunType.Primary)
            ShootingState.currentGun = ShootingState.GunType.Secondary;

        else if (ShootingState.currentGun == ShootingState.GunType.Secondary)
            ShootingState.currentGun = ShootingState.GunType.Primary;

        weaponSwitching.Switch();

        isSwitching = false;
    }
}
