using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public event Action<Action> OnPlayerTurn;
    public event Action<Action> OnWaiting;
    public event Action<Action> OnUnitTurn;

    public static BattleSystem Instance;

    public enum State
    {
        PlayerTurn,
        Waiting,
        UnitTurn
    }

    private State state;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = State.PlayerTurn;
    }

    private void Update()
    {
        switch (state)
        {
            case State.PlayerTurn:
                OnPlayerTurn?.Invoke(() => { state = State.Waiting; });
                break;
            case State.Waiting:
                OnWaiting?.Invoke(() => { state = State.UnitTurn; });
                break;
            case State.UnitTurn:
                OnUnitTurn?.Invoke(() => { state = State.PlayerTurn; });
                break;
            default:
                break;
        }
    }

    public bool IsPlayerTurn()
    {
        return state == State.PlayerTurn;
    }

    public bool IsUnitTurn()
    {
        return state == State.UnitTurn;
    }

    public bool IsWaiting()
    {
        return state == State.Waiting;
    }
}
