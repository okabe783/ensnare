using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private SelectedCard _selectedCard;
    [SerializeField] private HandPosition _hand;
    [SerializeField] private Player _player;
    [SerializeField] private UIManager _uiManager;

    private bool _isPanelActive;
    public Turn _turn { get; set; }

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
        if (_turn == Turn.RefreshPhase)
        {
            RefreshPhase();
        }
        else if (_turn == Turn.BattlePhase && _isPanelActive)
        {
            BattlePhase();
        }
        else if (_turn == Turn.EndPhase)
        {
            EndPhase();
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
        DrawCard(_player);

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
    }

    /// <summary>Turnを終了したときに呼び出される</summary>
    private void EndPhase()
    {
        _uiManager.SetTurnEndPanel();
    }

    private void IsUseCard()
    {
        if (_hand.IsEmpty() && _selectedCard._isSelectCard == null && _turn == Turn.MainPhase)
        {
            _turn = Turn.BattlePhase;
            _isPanelActive = true;
            Debug.Log("BattlePhase");
        }
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