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
        if (realState != null)
            return;

        await state.Enter(gameObject);
        currentState.Add(state);

    }

    
    public static T FindState<T>() where T : BaseState
    {
        //currentState is an array of Base States
        //Goes through every state in currentState and trys to find the one that we specified. ie We want moving state, it tries to find moving state.
        return currentState.Find(s => typeof(T) == s.GetType()) as T;      
    }

}
