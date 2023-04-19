using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public enum PLayerStateType 
{ 
    Idle,
    Walk,
    Jump,
    Attack,
    Fall
}


public class StateMachine<T> where T : Enum
{
    private Dictionary<T, IState> _states = new();
    public IState _currentState;

    public void RegisterState(T type, IState state)
    {
        if (_states.ContainsKey(type))
        {           
            throw new Exception("Stato gia Presente" + type);
        }
        _states.Add(type, state);
    }

    public void SetState(T type)
    {
      if(!_states.ContainsKey(type)) 
        {
            throw new Exception("Stato non registrato" + type);
        }

      _currentState?.OnExit();
      _currentState = _states[type];
        _currentState.OnEnter();
    }

    public void Update() => _currentState?.OnUpdate();

   
    }






