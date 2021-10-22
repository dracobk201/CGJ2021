using ScriptableObjectArchitecture;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private BoolReference turnTransition = default(BoolReference);
    [SerializeField] private GameEvent showingActions = default(GameEvent);
    [SerializeField] private GameEvent hideActions = default(GameEvent);
    [SerializeField] private GameEvent enemyChoosing = default(GameEvent);

    private Global.Turn _currentTurn;

    private void Start()
    {
        _currentTurn = Global.Turn.Player;
        showingActions.Raise();
    }

    public void PassTurn()
    {
        turnTransition.Value = true;
        if (_currentTurn.Equals(Global.Turn.Player))
        {
            hideActions.Raise();
            Invoke(nameof(EnemyTurn), 0.5f);
        }
        else
            Invoke(nameof(PlayerTurn), 1f);
    }

    private void PlayerTurn()
    {
        _currentTurn = Global.Turn.Player;
        turnTransition.Value = false;
        showingActions.Raise();
    }

    private void EnemyTurn()
    {
        _currentTurn = Global.Turn.Enemy;
        turnTransition.Value = false;
        enemyChoosing.Raise();
    }
}
