using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField]
    public BaseState[] initialState;
    private static List<BaseState> currentState = new List<BaseState>();

    public void Start()
    {
        currentState = initialState.ToList();
        foreach (var state in currentState)     
            state?.Enter(gameObject);
    }

    public void OnEnable()
    {
        BaseState._triggerState += TriggerState;
        BaseState._Stop += StopState;
    }

    public void OnDisable()
    {
        BaseState._triggerState -= TriggerState;
        BaseState._Stop -= StopState;
    }

    public async void StopState(BaseState state)
    {
        //loop through the currentstate and finds it then runs leave if not null
        var realState = currentState.Find(s => state == s);
        if (realState == null)
            return;

        await realState.Leave(gameObject);
        currentState.Remove(realState);
   
    }

    public void Update()
    {
        if (currentState == null)
            return;

        foreach (var state in currentState)
            state?.Actions(gameObject);
    }

    private async void TriggerState(BaseState state)
    {
        //loop through the currentstate and finds it then runs leave if not null
        var realState = currentState.Find(s => state == s);
        if (realState == null)
            return;

        await realState.Enter(gameObject);
        currentState.Add(realState);

    }

}
