using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderEnemy : MonoBehaviour
{ 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = GameObject.FindGameObjectWithTag("Enemy");
            enemy.GetComponent<Renderer>().enabled = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = GameObject.FindGameObjectWithTag("Enemy");
            enemy.GetComponent<Renderer>().enabled = false;
        }
    }
    /*
    GameObject[] enemies;

    void Awake()
    {

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void OnTriggerEnter(Collider other)
    {


        foreach (GameObject enemy in enemies)
        {

            enemy.GetComponent<Renderer>().enabled = true;

        }
    }
    */
}
