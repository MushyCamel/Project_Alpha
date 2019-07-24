using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public Objective[] objectives;

    private void Awake()
    {
        objectives = GetComponents<Objective>();
    }

    void OnGUI()
    {
        foreach (var objective in objectives)
        {
            objective.DrawHUD();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var objective in objectives)
        {
            if (objective.IsAchieved())
            {
                objective.Complete();
                Destroy(objective);
            }
        }
    }
}
