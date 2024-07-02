using Ensnare.Enums;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PunTurnManager))]
public class OnlineGameManager : MonoBehaviour, IPunTurnManagerCallbacks
{
    [SerializeField] private TurnPhase _turnPhase; //ターン中の各Phaseを管理
    [SerializeField] private PunTurnManager _punTurnManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Button _endTurnButton; //ターン終了ボタン。自分のターンの時のみinteractable になる

    public Button Button => _endTurnButton;

    private void Start()
    {
        _punTurnManager.GetComponent<PunTurnManager>();
        _punTurnManager.TurnManagerListener = this; // IPunTurnManagerCallbacksをこのクラスに設定する
        _endTurnButton.interactable = false; // 最初はターン終了ボタンを非活性化
    }

    public void EndTurn()
    {
        Debug.Log("EndTurnが呼び出されました");
        _punTurnManager.SendMove(null, true); // 行動を終了し、次のターンに進める
        _endTurnButton.interactable = false; // ターン終了ボタンを再度非活性化
    }

    /// <summary>Cardを引く</summary>
    public void Draw()
    {
        _uiManager.DrawCard();
    }

    private void BeginTurn()
    {
        _endTurnButton.interactable = true;
        _turnPhase.CurrentPhase = Phase.StartPhase;
    }

    //ターンの開始時
    void IPunTurnManagerCallbacks.OnTurnBegins(int turn)
    {
        Debug.LogFormat("OnTurnBegins {0}", turn); //現在が何ターン目であるか

        //Masterのターンからはじめる
        if (!PhotonNetwork.IsMasterClient) return;

        Debug.Log("マスターのturn");
        BeginTurn();
    }

    //プレイヤーの行動終了時
    void IPunTurnManagerCallbacks.OnPlayerFinished(Photon.Realtime.Player player, int turn, object move)
    {
        Debug.LogFormat("プレイヤー {0} が行動を終了しました: {1}", player.NickName, turn);

        // 自分が MasterClient ではなくて、一つ前の ActorNumber の人が行動終了した時に
        if (!PhotonNetwork.IsMasterClient && PhotonNetwork.LocalPlayer.ActorNumber == player.ActorNumber + 1)
        {
            Debug.Log(player.ActorNumber);
            BeginTurn(); // 自分のターンとみなす
        }
    }

    //SendMoveを呼び出したが行動を終了していない時
    void IPunTurnManagerCallbacks.OnPlayerMove(Photon.Realtime.Player player, int turn, object move)
    {
    }

    //参加しているプレイヤー全員が行動を終了した時
    void IPunTurnManagerCallbacks.OnTurnCompleted(int turn)
    {
        Debug.Log("1周しました");
        _punTurnManager.BeginTurn();
    }

    //ターンが時間切れになった時
    void IPunTurnManagerCallbacks.OnTurnTimeEnds(int turn)
    {
    }
}