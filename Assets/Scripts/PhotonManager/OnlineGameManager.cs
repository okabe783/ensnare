using UnityEngine;
using Photon.Pun;
using Ensnare.Enums;
using Photon.Pun.UtilityScripts;

[RequireComponent(typeof(PunTurnManager))]
public class OnlineGameManager : MonoBehaviour, IPunTurnManagerCallbacks
{
    [SerializeField] private TurnPhase _turn; //ターン中の各Phaseを管理
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PunTurnManager _punTurnManager;
    [SerializeField] private TurnChange _turnChange; // TurnChangeクラスの参照を追加
    
    private Phase _currentPhase = Phase.none; //現在のターンを保持
    

    private void Start()
    {
        _punTurnManager.GetComponent<PunTurnManager>();
        _punTurnManager.TurnManagerListener = this; // IPunTurnManagerCallbacks をこのクラスに設定する
    }

    public void StartTurn()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _turn.CurrentPhase = Phase.StartPhase;
        }
    }
    
    public void EndTurn()
    {
        if (_punTurnManager != null)
        {
            _punTurnManager.SendMove(null, true); // 行動を終了し、次のターンに進める
        }
    }

    /// <summary>Cardを引く</summary>
    public void Draw()
    {
        _uiManager.DrawCard();
    }
    
    //ターンの開始時
    void IPunTurnManagerCallbacks.OnTurnBegins(int turn)
    {
        Debug.Log("Turn begins for turn number: " + turn);
        StartTurn();
    }
    //プレイヤーの行動終了時
    void IPunTurnManagerCallbacks.OnPlayerFinished(Photon.Realtime.Player player, int turn, object move)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _turnChange.GuestTurn();
        }
        else
        {
            _turnChange.MasterTurn();
        }
    }
    //SendMoveを呼び出したが行動を終了していない時
    void IPunTurnManagerCallbacks.OnPlayerMove(Photon.Realtime.Player player, int turn, object move) { }
    //参加しているプレイヤー全員が行動を終了した時
    void IPunTurnManagerCallbacks.OnTurnCompleted(int turn) { }
    //ターンが時間切れになった時
    void IPunTurnManagerCallbacks.OnTurnTimeEnds(int turn) { }
}