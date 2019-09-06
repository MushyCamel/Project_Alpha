using UnityEngine;

public interface IAbility
{
    void ActivateAbility();
    void CoolDown(float _coolDownDuration);
}
