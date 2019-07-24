using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective : MonoBehaviour
{
    public abstract bool IsAchieved();
    public abstract void Complete();
    public abstract void DrawHUD();
}
