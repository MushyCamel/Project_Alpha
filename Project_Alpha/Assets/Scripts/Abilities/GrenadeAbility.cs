using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/GrenadeAbility")]
public class GrenadeAbility : Ability
{
    public float h = 4f;
    public ForceMode _forceMode;
    public float _blastRadius = 20f;
    public float _force = 100f;
    public float _damage = 100f;

    private Grenade gAbility;

    public override void Initialize(GameObject gameObject)
    {
        gAbility = gameObject.GetComponent<Grenade>();
        //gAbility.Initialize();

        gAbility._blastRadius = _blastRadius;
        gAbility._force = _force;
        gAbility._forceMode = _forceMode;
        gAbility._damage = _damage;
        gAbility.h = h;

    }

    public override void TriggerAbility()
    {
        gAbility.Fire();
    }


}
