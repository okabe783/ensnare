using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private Player _player;
    [SerializeField] private UIManager _uiManager;
    public Turn turn;

    private void Start()
    {
        //シングルトン化
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        SetUp();
    }

    private void FixedUpdate()
    {
        if (turn == Turn.RefreshPhase)
        {
            RefreshPhase();
        }
        else if (turn == Turn.BattlePhase)
        {
            BattlePhase();
        }
        else if (turn == Turn.EndPhase)
        {
            EndPhase();
        }
    }

    private void SetUp()
    {
        _characterManager.CreatePlayerObject(); 
        DrawCard(_player);
        MainPhase();
        turn = Turn.MainPhase;
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
    }

    /// <summary>Turnを終了したときに呼び出される</summary>
    private void EndPhase()
    {
        _uiManager.SetTurnEndPanel();
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