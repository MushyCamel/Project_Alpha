using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrenadeAbility : Ability
{
    public float _grenadeDamage = 30f;
    public float _throwRange = 40f;
    public float _blastRadius = 10f;

    private Grenade gAbility;

    public override void Initialize(GameObject gameObject)
    {
        gAbility = gameObject.GetComponent<Grenade>();
    }

    public override void TriggerAbility()
    {
        throw new System.NotImplementedException();
    }


}
