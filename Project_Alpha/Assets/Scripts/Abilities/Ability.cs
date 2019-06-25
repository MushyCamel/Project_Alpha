using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string aName = "New Ability";
    public Sprite aSprite;
    public float aBaseCoolDown = 1f;


    public abstract void TriggerAbility();
    public abstract void Initialize(GameObject gameObject);

}
