using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectorUI : MonoBehaviour
{
    public GameObject[] player;
    public Vector3 playerSpawnPosition = new Vector3(0, 1, -7);
    public Character[] characters;
    //public GameObject characterPrefab;

    public GameObject characterSelectPanel;
    public GameObject abilityPanel;


    public void OnCharacterSelect(int characterChoice)
    {
        characterSelectPanel.SetActive(false);
        abilityPanel.SetActive(true);
        GameObject spawnedPlayer = Instantiate(player[characterChoice], playerSpawnPosition, Quaternion.identity) as GameObject;
        WeaponMarker weaponMarker = spawnedPlayer.GetComponentInChildren<WeaponMarker>();
        AbilityCooldown[] coolDownButtons = GetComponentsInChildren<AbilityCooldown>();
        Character selectedCharacter = characters[characterChoice];
        for (int i = 0; i < coolDownButtons.Length; i++)
        {
            coolDownButtons[i].Initialize(selectedCharacter.characterAbilities[i], weaponMarker.gameObject);
        }
    }
}
