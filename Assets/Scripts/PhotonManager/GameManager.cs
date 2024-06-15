using Ensnare.Enums;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private PhotonObjectManager _photonObjectManager;
    [SerializeField] private HandPosition _handPosition;
    [SerializeField] private Battle _battle;
    [SerializeField] private SelectedCard _selectedCard;
    [SerializeField] private HandPosition _hand;
    public bool _isPanelActive { get; set; }　//Phaseごとに呼び出すPanelの判定

    public Phase _turn { get; set; }　//いまがどのターンなのか

    private void Start()
    {
        SetUp();
    }

    private void Update()
    {
        switch (_turn)
        {
            case Phase.StartPhase:
                //SetUpNextTurn(_player);
                break;
            case Phase.RefreshPhase:
                //RefreshPhase();
                break;
            //battlePhaseでパネルがアクティブになっていなければ
            case Phase.BattlePhase when _isPanelActive:
                BattlePhase();
                break;
            case Phase.EndPhase when _isPanelActive:
                EndPhase();
                break;
        }
    }

    private void FixedUpdate()
    {
        IsUseCard();
    }

    private void SetUp()
    {
        _turn = Phase.MainPhase;
    }
    
    /// <summary>Battleを行う</summary>
    private void BattlePhase()
    {
    }

    /// <summary>Turnを終了したときに呼び出される</summary>
    private void EndPhase()
    {
        //_panel.SetTurnEndPanel();
        _isPanelActive = false;
        //_panel._button.SetActive(false);
        _turn = Phase.StartPhase;
    }

    /// <summary>Cardを全て使用していたならBattlePhaseに移行する</summary>
    private void IsUseCard()
    {
        if (_hand.IsEmpty() && _selectedCard._isSelectCard == null && _turn == Phase.MainPhase)
        {
            _turn = Phase.BattlePhase;
            _isPanelActive = true;
        }
    }
}