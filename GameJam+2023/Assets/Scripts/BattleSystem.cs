using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public event Action<Action> OnStateChanged;

    public static BattleSystem Instance;

    public enum State
    {
        PlayerTurn,
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
                OnStateChanged?.Invoke(() => { state = State.UnitTurn; });
                break;
            case State.UnitTurn:
                OnStateChanged?.Invoke(() => { state = State.PlayerTurn; });
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
}
