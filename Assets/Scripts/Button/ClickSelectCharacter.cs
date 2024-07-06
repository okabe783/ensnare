using Photon.Pun;
using UnityEngine;
using Ensnare.Enums;
using UnityEngine.EventSystems;

/// <summary>Characterが押された際の処理</summary>
public class ClickSelectCharacter : MonoBehaviourPunCallbacks, IPointerClickHandler
{
    [SerializeField] private CharacterValue _characterValue;
    [SerializeField] private TurnPhase _turnPhase;
    [SerializeField] private OnlineGameManager _onlineGameManager;
    [SerializeField] private CharacterValue characterValue;

    //手札のPosition
    [SerializeField] private HandPosition _masterHandPosition;
    [SerializeField] private HandPosition _guestHandPosition;

    //Clickしたカードを移動させるエリア
    [SerializeField] private SelectPosition _masterSelectPosition;
    [SerializeField] private SelectPosition _guestSelectPosition;

    public int PowerValue => _characterValue.PowerValue;

    /// <summary>CharacterがClickされたときの処理</summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(_characterValue.PowerValue);
        //ターンプレイヤーのみが選択できる
        if (!_onlineGameManager.Button.interactable) return; 

        //BattlePhaseに移行していたらBattleを開始する
        if (_turnPhase.CurrentPhase == Phase.BattlePhase)
            SelectBattle(this); 

        //ポジションの指定
        var selectPosition = PhotonNetwork.IsMasterClient 
            ? _masterSelectPosition : _guestSelectPosition;

        //カードが選択中ではないまたはMainPhaseではない場合
        if (selectPosition == null || selectPosition.SelectCard == null 
                                   || _turnPhase.CurrentPhase != Phase.MainPhase) return;

        //カードのPower分上昇
        _characterValue.GetPower(selectPosition.SelectCard.Power);
        //characterValue.PowerValue += selectPosition.SelectCard.Power; 
        
        //TrapCardをセット
        if (selectPosition.SelectCard.CardType == CardType.TrapCard 
            && _onlineGameManager.IsFirstTrap == false)
        {
            _onlineGameManager.IsFirstTrap = true;
        }

        //ToDo:boolで管理しているとTrueにはなるので別のCharacterにいれてもTrueになる
        else if (selectPosition.SelectCard.CardType == CardType.TrapCard && _onlineGameManager.IsFirstTrap)
        {
            //_onlineGameManager.IsSecondTrap = true;
            Debug.Log("2枚目のTrapカードをセット");
        }

        selectPosition.DestroyCard();　//カードを削除
        photonView.RPC(nameof(OnRemoveCard), 
            RpcTarget.Others, PhotonNetwork.IsMasterClient);
        CheckBattlePhase();
    }

    /// <summary>手札のカードを使用したことを通知</summary>
    [PunRPC]
    public void OnRemoveCard(bool isMaster)
    {
        var handPos = isMaster ? _masterHandPosition : _guestHandPosition;
        if (handPos == null || handPos.IsEmpty(isMaster)) return;

        var cardToRemove = handPos.GetRandomCard(isMaster);

        if (cardToRemove == null) return;
        handPos.RemoveCard(cardToRemove, isMaster);
        Destroy(cardToRemove.gameObject);
    }

    //手札のカードがなくなったらBattlePhaseに移行する
    private void CheckBattlePhase()
    {
        var handPos = PhotonNetwork.IsMasterClient ? _masterHandPosition : _guestHandPosition;

        if (handPos.IsEmpty(PhotonNetwork.IsMasterClient))
        {
            photonView.RPC(nameof(TransitionToBattlePhase), RpcTarget.All, PhotonNetwork.IsMasterClient);
        }
    }

    /// <summary>BattlePhaseに移行したことを通知</summary>
    [PunRPC]
    public void TransitionToBattlePhase(bool isMaster)
    {
        if (!isMaster)
        {
            // マスタークライアントのフェーズをバトルフェーズに変更
            _turnPhase.CurrentPhase = Phase.BattlePhase;
            _turnPhase.ActiveBattlePhase();
        }
        else
        {
            // ゲストクライアントのフェーズをバトルフェーズに変更
            _turnPhase.CurrentPhase = Phase.BattlePhase;
            _turnPhase.ActiveBattlePhase();
        }
    }

    //選択したキャラクターをリストに格納
    private void SelectBattle(ClickSelectCharacter character)
    {
        Debug.Log(_onlineGameManager.IsFirstTrap);
        if (_turnPhase.CurrentPhase != Phase.BattlePhase) return;
        _turnPhase.Select(character);
    }
}