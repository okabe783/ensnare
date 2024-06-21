using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Ensnare.Enums;
using Photon.Pun.UtilityScripts;

[RequireComponent(typeof(PunTurnManager))]
public class OnlineGameManager : MonoBehaviour, IPunTurnManagerCallbacks
{
    [SerializeField] private TurnPhase _turn; //ターン中の各Phaseを管理
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PunTurnManager _punTurnManager;
    
    private Phase _currentPhase = Phase.StartPhase; //現在のターンを保持

    /// <summary>Cardを引く</summary>
    public void Draw()
    {
        _uiManager.DrawCard();
    }
    
    //ターンの開始時
    void IPunTurnManagerCallbacks.OnTurnBegins(int turn)
    {
        //Masterのターンからスタート
        if (PhotonNetwork.IsMasterClient)
        {
            _turn.CurrentPhase = Phase.StartPhase;
        }
    }
    //プレイヤーの行動終了時
    void IPunTurnManagerCallbacks.OnPlayerFinished(Photon.Realtime.Player player, int turn, object move) { }
    //SendMoveを呼び出したが行動を終了していない時
    void IPunTurnManagerCallbacks.OnPlayerMove(Photon.Realtime.Player player, int turn, object move) { }
    //参加しているプレイヤー全員が行動を終了した時
    void IPunTurnManagerCallbacks.OnTurnCompleted(int turn) { }
    //ターンが時間切れになった時
    void IPunTurnManagerCallbacks.OnTurnTimeEnds(int turn) { }
}