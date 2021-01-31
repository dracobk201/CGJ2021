using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private GameEvent showingActions = null;
    [SerializeField] private GameEvent hideActions = null;
    [SerializeField] private GameEvent enemyChoosing = null;
    private Global.Turn _currentTurn;

    private void Start()
    {
        _currentTurn = Global.Turn.Player;
        showingActions.Raise();
    }

    public void PassTurn()
    {
        if (_currentTurn.Equals(Global.Turn.Player))
        {
            hideActions.Raise();
            Invoke(nameof(EnemyTurn), 0.5f);
        }
        else
            Invoke(nameof(PlayerTurn), 0.5f);
    }

    private void PlayerTurn()
    {
        _currentTurn = Global.Turn.Player;
        showingActions.Raise();
    }

    private void EnemyTurn()
    {
        _currentTurn = Global.Turn.Enemy;
        enemyChoosing.Raise();
    }
}
