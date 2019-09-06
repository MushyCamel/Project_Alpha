using UnityEngine;
using UnityEngine.UI;

public class AbilityBaseClass: MonoBehaviour
{
    public string abilityButtonAxisName = "Ability 1";
    public Image darkMask;
    public Text coolDownTextDisplay;

    public Sprite aSprite;

    private GameObject _weaponHolder;
    private Image _myButtonImage;
    private float _coolDownDuration;
    private float _nextReadyTime;
    private float _coolDownTimeLeft;


    void Start()
    {

    }

    public void Initialize(GameObject _weaponHolder, float aBaseCoolDown)
    {
        _myButtonImage = GetComponent<Image>();
        _myButtonImage.sprite = aSprite;
        darkMask.sprite = aSprite;
        _coolDownDuration = aBaseCoolDown;
        Initialize(_weaponHolder, aBaseCoolDown);
        AbilityReady();
    }

    public virtual void ActivateAbility()
    {

    }

    protected void CoolDown(float aBaseCoolDown)
    {
        //reduce the amount of cooldown left
        _coolDownTimeLeft -= Time.deltaTime;

        //make sure only whole numbers are shown on the UI
        float roundedCD = Mathf.Round(_coolDownTimeLeft);
        //Set the cooldown to the text and set it the number between 1 and 0 for the fill amount.
        coolDownTextDisplay.text = roundedCD.ToString();
        darkMask.fillAmount = (_coolDownTimeLeft / _coolDownDuration);
    }

    protected void AbilityReady()
    {
        coolDownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    protected void ButtonTriggered()
    {
        _nextReadyTime = _coolDownDuration + Time.time;
        _coolDownTimeLeft = _coolDownDuration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;
    }

}
