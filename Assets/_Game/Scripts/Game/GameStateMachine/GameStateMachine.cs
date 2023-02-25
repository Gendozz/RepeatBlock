using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
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
        //Debug.Log($"--- ENTER --- {typeof(TState)} GSM");
    }
}
