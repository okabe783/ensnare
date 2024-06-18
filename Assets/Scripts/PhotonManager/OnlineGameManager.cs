using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

[RequireComponent(typeof(PunTurnManager))]
public class OnlineGameManager : MonoBehaviour, IPunTurnManagerCallbacks
{
    private PunTurnManager _punTurnManager;

    private void Start()
    {
        _punTurnManager = GetComponent<PunTurnManager>();
    }

    //ターンの開始時
    void IPunTurnManagerCallbacks.OnTurnBegins(int turn) { }
    //プレイヤーの行動終了時
    void IPunTurnManagerCallbacks.OnPlayerFinished(Photon.Realtime.Player player, int turn, object move) { }
    //SendMoveを呼び出したが行動を終了していない時
    void IPunTurnManagerCallbacks.OnPlayerMove(Photon.Realtime.Player player, int turn, object move) { }
    //参加しているプレイヤー全員が行動を終了した時
    void IPunTurnManagerCallbacks.OnTurnCompleted(int turn) { }
    //ターンが時間切れになった時
    void IPunTurnManagerCallbacks.OnTurnTimeEnds(int turn) { }
}