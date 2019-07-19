using System;
using System.Threading.Tasks;
using UnityEngine;

public interface IState
{
    void Actions(GameObject player);
    Task Enter(GameObject player);
    Task Leave(GameObject player);
}

[Serializable]
 public abstract class BaseState : ScriptableObject, IState
{
    public static event Action<BaseState> _triggerState;
    public static event Action<BaseState> _Stop;

    public void TriggerState(BaseState state)
    {
        _triggerState(state);
    }

    public void Stop()
    {
        _Stop(this);
    }

    public abstract void Actions(GameObject player);
    public abstract Task Enter(GameObject player);
    public abstract Task Leave(GameObject player);
}