using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private CardGenerator _cardGenerator; //Cardを生成するクラス
    [SerializeField] private CharacterManager _characterManager;　//battleCharacterに関するManager
    [SerializeField] private HandPosition _handPosition;
    [SerializeField] private SelectedCard _selectedCard;
    [SerializeField] private HandPosition _hand;
    [SerializeField] private Player _player;
    [SerializeField] private UIManager _uiManager;
    public bool _isPanelActive { get; set; }　//Phaseごとに呼び出すPanelの判定

    public Turn _turn { get; set; }　//いまがどのターンなのか

    private void Start()
    {
        //シングルトン化
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        SetUp();
    }

    private void Update()
    {
        switch (_turn)
        {
            case Turn.StartPhase:
                SetUpNextTurn(_player);
                break;
            case Turn.RefreshPhase:
                RefreshPhase();
                break;
            //battlePhaseでパネルがアクティブになっていなければ
            case Turn.BattlePhase when _isPanelActive:
                BattlePhase();
                break;
            case Turn.EndPhase when _isPanelActive:
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
        _characterManager.CreatePlayerObject();
        DrawCard(_player);
        MainPhase();
        _turn = Turn.MainPhase;
    }

    /// <summary>GameがStartしたときhandを配る</summary>
    private void DrawCard(Player player)
    {
        for (var i = 0; i < 6; i++)
        {
            var card = _cardGenerator.CardSpawn(i); //Cardを配る
            player.SetCardToHand(card);
        }

        player.Hand.ResetPosition();
    }

    /// <summary>自分のターン開始時playerの情報をresetする</summary>
    private void RefreshPhase()
    {
        _uiManager.SetRefreshPanel();

        foreach (var character in _characterManager.characterList)
        {
            character._powerValue = 0;
        }
    }

    /// <summary>CardをセットするTurn</summary>
    private void MainPhase()
    {
        _uiManager.SetMainPanel();
    }

    /// <summary>Battleを行う</summary>
    private void BattlePhase()
    {
        _uiManager.SetBattlePanel();
        _isPanelActive = false;
        _uiManager._button.SetActive(true);
    }

    /// <summary>Turnを終了したときに呼び出される</summary>
    private void EndPhase()
    {
        _uiManager.SetTurnEndPanel();
        _isPanelActive = false;
        _uiManager._button.SetActive(false);
        _turn = Turn.StartPhase;
    }

    /// <summary>Cardを全て使用していたならBattlePhaseに移行する</summary>
    private void IsUseCard()
    {
        if (_hand.IsEmpty() && _selectedCard._isSelectCard == null && _turn == Turn.MainPhase)
        {
            _turn = Turn.BattlePhase;
            _isPanelActive = true;
        }
    }

    /// <summary>2ターン目以降のスタート時に呼び出される</summary>
    private void SetUpNextTurn(Player player)
    {
        _handPosition.ResetCard();
        DrawCard(player);
        MainPhase();
        _turn = Turn.RefreshPhase;
    }

    public enum Turn
    {
        StartPhase,
        RefreshPhase,
        MainPhase,
        BattlePhase,
        EndPhase,
    }
}