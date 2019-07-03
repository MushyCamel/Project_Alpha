using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockShot : MonoBehaviour
{

    public Transform target;
    public float range = 60;

    public string enemyTag = "Enemy";
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        layerMask = LayerMask.GetMask("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float distanceToEnemy = Mathf.Infinity;
        GameObject targetEnemy = null;

        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit enemyHit;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < distanceToEnemy)
            {
                distanceToEnemy = distance;
            }

            if (Physics.Raycast(camRay, out enemyHit, 100f, layerMask))
            {
                Vector3 playerToMouse = enemyHit.point - transform.position;

                targetEnemy = enemy;
                if (Input.GetButtonDown("Ability 1"))
                {
                    if (distanceToEnemy <= range && targetEnemy != null)
                    {
                        target = targetEnemy.transform;
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
