using System.Collections;
using Ensnare.Enums;
using Photon.Pun;
using UnityEngine;
using UniRx;

/// <summary>各Phaseを管理</summary>
public class TurnPhase : MonoBehaviour
{
    [SerializeField] private StartPhase _startPhase;
    [SerializeField] private MainPhase _mainPhase;
    [SerializeField] private BattlePhase _battlePhase;
    [SerializeField] private EndPhase _endPhase;
    [SerializeField] private OnlineGameManager _onlineGameManager;
    
    private ReactiveProperty<Phase> _currentPhase = new(Phase.none);

    private PhotonView _photonView;
    public Phase CurrentPhase
    {
        get => _currentPhase.Value;
        set => _currentPhase.Value = value;
    }

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        // _currentPhase.ObserveEveryValueChanged(phase => phase.Value)
        //     .Subscribe(SetPhasePanel).AddTo(this);
    }

    /// <summary>各Phaseに合わせてPanelをアクティブにする</summary>
    public void SetPhasePanel(Phase currentPhase)
    {
        Debug.Log(_onlineGameManager.Button.interactable);
        if(!_onlineGameManager.Button.interactable) return;
        
        switch (currentPhase)
        {
            case Phase.StartPhase:
                _startPhase.StartPhaseSetUp();
                break;
            case Phase.MainPhase:
                StartCoroutine(ChangeMainPhase());
                break;
            case Phase.BattlePhase:
                _battlePhase.BattlePhaseSetUp();
                break;
            case Phase.EndPhase:
                _endPhase.EndPhaseSetUp();
                break;
        }
    }

    //MainPhaseに移行するのを遅らせる
    private IEnumerator ChangeMainPhase()
    {
        yield return new WaitForSeconds(2);
        _mainPhase.MainPhaseSetUp();
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