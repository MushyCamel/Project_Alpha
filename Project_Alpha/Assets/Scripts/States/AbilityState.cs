using System.Threading.Tasks;
using UnityEngine;
//CHANGE ABILITY MANAGEMENT - Use interfaces or inheritance?, remove scriptable objects. 
[CreateAssetMenu(menuName = "States/AbilityState")]
public class AbilityState : BaseState
{
    public enum AbilityClass
    {
        assault = 0,
        sniper = 1,
        shotgun = 2,
    }

    public static AbilityClass currentAbility = AbilityClass.assault;

    public MovingState movingState;
    public ShootingState shootingState;

    public override async Task Enter(GameObject player)
    {
        //if (primaryGun == null)
        //    primaryGun = player.GetComponentInChildren<MachinegunScript>();
        //if (primaryGun == null)
        //    primaryGun = player.GetComponentInChildren<SniperScript>();
        //if (primaryGun == null)
        //    primaryGun = player.GetComponentInChildren<ShotgunScript>();

        //if (secondaryGun == null)
        //    secondaryGun = player.GetComponentInChildren<PistolScript>();

        await new WaitForEndOfFrame();
        Debug.Log("entered state " + nameof(ShootingState));
    }

    public override void Actions(GameObject player)
    {

    }

    public override async Task Leave(GameObject player)
    {
        await new WaitForEndOfFrame();
        Debug.Log("leaving State");
    }
}
