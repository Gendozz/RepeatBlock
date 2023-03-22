using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameStateMachine
{
    [DllImport("__Internal")]
    private static extern void PrintToConsole(string textToprint);

    private readonly Dictionary<Type, IState> _states;
    private IState _activeState;

    public GameStateMachine()
    {
        _states= new Dictionary<Type, IState>();
    }

    public void AddState<TState>(TState state) where TState : class, IState
    {
        _states.Add(typeof(TState), state);
    }

    public void ChangeState<TState>() where TState : class, IState
    {
        _activeState?.Exit();
        //Debug.Log($"--- EXIT --- {typeof(TState)} GSM");
        try
        {
            _activeState = _states[typeof(TState)] as TState;
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogError($"Message {e.Message}. {typeof(TState)} GSM");
            throw;
        }
        
        _activeState.Enter();

#if !UNITY_EDITOR
        PrintToConsole($"--- ENTER --- {typeof(TState)} GSM"); 
#endif
        //Debug.Log($"--- ENTER --- {typeof(TState)} GSM");
    }
}
