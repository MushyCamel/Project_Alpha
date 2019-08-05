using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemies : Objective
{

    public int requiredKills = 50;

    private PlayerStats playerStats;

    void Awake()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    public override bool IsAchieved()
    {
        return (playerStats.kills >= requiredKills);
    }

    public override void Complete()
    {
        Debug.Log("Completed");
    }

    public override void DrawHUD()
    {
        GUILayout.Label(string.Format("Killed {0}/{1} enemies", playerStats.kills, requiredKills));
    }
}
