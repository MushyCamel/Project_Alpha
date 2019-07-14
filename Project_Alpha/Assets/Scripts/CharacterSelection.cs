using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] _characterList;
    [HideInInspector]
    public int _index;
    

    // Start is called before the first frame update
    private void Start()
    {
        Initiate();
    }

    private void Initiate()
    {
        _index = PlayerPrefs.GetInt("CharacterSelected");

        _characterList = new GameObject[transform.childCount];

        //fill the array
        for (int i = 0; i < transform.childCount; i++)
        {
            _characterList[i] = transform.GetChild(i).gameObject;
        }

        //toggle off the renderer
        foreach (GameObject go in _characterList)
        {
            go.SetActive(false);
        }

        //toggle on the Selected character
        if (_characterList[_index])
            _characterList[_index].SetActive(true);
    }

    public void ToggleLeft()
    {
        //toggle off current model
        _characterList[_index].SetActive(false);

        _index--;
        if (_index < 0)
            _index = _characterList.Length - 1;

        //toggle new model
        _characterList[_index].SetActive(true);
    }

    public void ToggleRight()
    {
        //toggle off current model
        _characterList[_index].SetActive(false);

        _index++;
        if (_index == _characterList.Length)
            _index = 0;

        //toggle new model
        _characterList[_index].SetActive(true);
    }

    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", _index);
        SceneManager.LoadScene("SampleScene");

    }
}
