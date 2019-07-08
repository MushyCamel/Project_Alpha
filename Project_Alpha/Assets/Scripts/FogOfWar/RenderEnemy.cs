using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderEnemy : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {

            other.GetComponent<MeshRenderer>().enabled = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            other.GetComponent<MeshRenderer>().enabled = false;

        }
    } 
}
