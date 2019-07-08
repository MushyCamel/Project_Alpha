using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    public string abilityButtonAxisName = "Ability 1";
    public Image darkMask;
    public Text coolDownTextDisplay;

    private Ability _ability;
    private GameObject _weaponHolder;
    private Image _myButtonImage;
    private float _coolDownDuration;
    private float _nextReadyTime;
    private float _coolDownTimeLeft;


    void Start()
    {
        Initialize(_ability, _weaponHolder);
    }

    public void Initialize(Ability selectedAbility, GameObject _weaponHolder)
    {
        _ability = selectedAbility;
        _myButtonImage = GetComponent<Image>();
        _myButtonImage.sprite = _ability.aSprite;
        darkMask.sprite = _ability.aSprite;
        _coolDownDuration = _ability.aBaseCoolDown;
        _ability.Initialize(_weaponHolder);
        AbilityReady();
    }

    // Update is called once per frame
    void Update()
    {
        bool coolDownComplete = (Time.time > _nextReadyTime);
        if (coolDownComplete)
        {
            AbilityReady();
            if (Input.GetButtonUp (abilityButtonAxisName))
            {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        coolDownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown()
    {
        //reduce the amount of cooldown left
        _coolDownTimeLeft -= Time.deltaTime;

        //make sure only whole numbers are shown on the UI
        float roundedCD = Mathf.Round(_coolDownTimeLeft);
        //Set the cooldown to the text and set it the number between 1 and 0 for the fill amount.
        coolDownTextDisplay.text = roundedCD.ToString();
        darkMask.fillAmount = (_coolDownTimeLeft / _coolDownDuration);
    }

    private void ButtonTriggered()
    {
        _nextReadyTime = _coolDownDuration + Time.time;
        _coolDownTimeLeft = _coolDownDuration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;

        _ability.TriggerAbility();
    }


}
