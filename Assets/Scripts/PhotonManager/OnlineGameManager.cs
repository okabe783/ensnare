using UnityEngine;
using UnityEngine.UI;
using Ensnare.Enums;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

[RequireComponent(typeof(PunTurnManager))]
public class OnlineGameManager : MonoBehaviour, IPunTurnManagerCallbacks
{
    [SerializeField] private TurnPhase _turnPhase; //ターン中の各Phaseを管理
    
    //Manager
    [SerializeField] private PunTurnManager _punTurnManager;
    [SerializeField] private UIManager _uiManager;
    
    [SerializeField] private Button _endTurnButton; //ターン終了ボタン。自分のターンの時のみinteractableになる
    public Button Button => _endTurnButton;

    public bool IsBind { get; set; }

    public int DownPowerValue { get; set; } = 0;

    private void Start()
    {
        _punTurnManager.GetComponent<PunTurnManager>();
        _punTurnManager.TurnManagerListener = this; // IPunTurnManagerCallbacksをこのクラスに設定する
        _endTurnButton.interactable = false;
        IsBind = false;
    }

    public void TurnEnd()
    {
        _punTurnManager.SendMove(null, true); // 行動を終了し、次のターンに進める
        _endTurnButton.interactable = false; 
    }

    /// <summary>Cardを引く</summary>
    public void Draw()
    {
        _uiManager.DrawCard();
    }

    //自分のターンを開始する
    private void BeginTurn()
    {
        _turnPhase.CurrentPhase = Phase.StartPhase;
        _endTurnButton.interactable = true;
    }

    //ターンの開始時に呼び出される
    void IPunTurnManagerCallbacks.OnTurnBegins(int turn)
    {
        Debug.LogFormat("OnTurnBegins {0}", turn); //現在が何ターン目であるか
        
        if (!PhotonNetwork.IsMasterClient) return;　//Masterのターンからはじめる
        BeginTurn();
    }

    //プレイヤーの行動終了時
    void IPunTurnManagerCallbacks.OnPlayerFinished(Photon.Realtime.Player player, int turn, object move)
    {
        Debug.LogFormat("プレイヤー {0} が行動を終了しました: {1}", player.NickName, turn);

        // 自分が MasterClient ではなくて、一つ前の ActorNumber の人が行動終了した時に
        if (!PhotonNetwork.IsMasterClient && PhotonNetwork.LocalPlayer.ActorNumber == player.ActorNumber + 1)
            BeginTurn(); // 自分のターンとみなす
    }

    //参加しているプレイヤー全員が行動を終了した時
    void IPunTurnManagerCallbacks.OnTurnCompleted(int turn)
    {
        _punTurnManager.BeginTurn();
    }

    //SendMoveを呼び出したが行動を終了していない時
    void IPunTurnManagerCallbacks.OnPlayerMove(Photon.Realtime.Player player, int turn, object move) { }
    //ターンが時間切れになった時
    void IPunTurnManagerCallbacks.OnTurnTimeEnds(int turn) { }
}