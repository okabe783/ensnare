using Ensnare.Enums;
using UnityEngine;

/// <summary>各Phaseを管理</summary>
public class TurnPhase : MonoBehaviour
{
    [SerializeField] private StartPhase _startPhase;
    [SerializeField] private MainPhase _mainPhase;
    [SerializeField] private BattlePhase _battlePhase;
    [SerializeField] private EndPhase _endPhase;

    /// <summary>各Phaseに合わせてPanelをアクティブにする</summary>
    public void SetPhasePanel(Phase currentPhase)
    {
        switch (currentPhase)
        {
            case Phase.StartPhase:
                _startPhase.StartPhaseSetUp();
                break;
            case Phase.MainPhase:
                _mainPhase.MainPhaseSetUp();
                break;
            case Phase.BattlePhase:
                _battlePhase.BattlePhaseSetUp();
                break;
                case Phase.EndPhase:
                    _endPhase.EndPhaseSetUp();
                    break;
        }
    }

    /// <summary>先行2ターン目以降はカードをリセットする必要がある</summary>
    public void AfterStart(Phase currentPhase)
    {
        if (currentPhase == Phase.AfterSecondStart)
        {
            _startPhase.SetUpNextTurn();
        }
    }

    /// <summary>Characterを選択したらリストに保存して2体選んだらバトルを開始する</summary>
    public void Select(ClickSelectCharacter character)
    {
        _battlePhase.SelectCharacter(character);
    }
}