using ScriptableObjectArchitecture;
using System.Collections;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private BoolReference isCombatOver = default(BoolReference);
    [SerializeField] private GameEvent showCombatIntro = default(GameEvent);
    [SerializeField] private GameEvent showingActions = default(GameEvent);
    [SerializeField] private GameEvent hideActions = default(GameEvent);
    [SerializeField] private GameEvent enemyChoosing = default(GameEvent);

    private Phase _currentPhase;

    private void Start()
    {
        _currentPhase = Phase.CombatIntro;
        TurnProgression();
    }

    public void TurnProgression()
    {
        TurnTransition();
    }

    private IEnumerator EnemyEndedNowPlayerTurn()
    {
        _currentPhase = Phase.PlayerTurn;
        yield return new WaitForSeconds(2f);
        showingActions.Raise();
    }

    private IEnumerator PlayerEndedNowEnemyTurn()
    {
        hideActions.Raise();
        _currentPhase = Phase.EnemyTurn;
        enemyChoosing.Raise();
        yield return new WaitForSeconds(2f);
    }

    private void TurnTransition()
    {
        switch (_currentPhase)
        {
            case Phase.CombatIntro:
                showCombatIntro.Raise();
                showingActions.Raise();
                _currentPhase = Phase.CombatIntro;
                _currentPhase = Phase.PlayerTurn;
                break;
            case Phase.PlayerTurn:
                StartCoroutine(PlayerEndedNowEnemyTurn());
                break;
            case Phase.EnemyTurn:
                StartCoroutine(EnemyEndedNowPlayerTurn());
                break;
            case Phase.CombatConclusion:
                break;
        }
    }
}

public enum Phase
{
    CombatIntro,
    PlayerTurn,
    EnemyTurn,
    CombatConclusion
}
