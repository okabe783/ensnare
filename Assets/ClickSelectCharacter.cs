using Photon.Pun;
using UnityEngine;
using Ensnare.Enums;
using UnityEngine.EventSystems;

/// <summary>Characterが押された際の処理</summary>
public class ClickSelectCharacter : MonoBehaviourPunCallbacks, IPointerClickHandler, IClick
{
    private TurnPhase _turnPhase;
    private OnlineGameManager _onlineGameManager;

    //手札のPosition
    private HandPosition _masterHandPosition;
    private HandPosition _guestHandPosition;

    //Clickしたカードを移動させるエリア
    private SelectPosition _masterSelectPosition;
    private SelectPosition _guestSelectPosition;
    public int PowerValue { get; set; } //Power加算用の変数

    private void Start()
    {
        var masterObject = GameObject.Find("ChoiceMasterCardArea");
        var masterHandObject = GameObject.Find("MasterHandPos");
        var guestObject = GameObject.Find("ChoiceGuestCardArea");
        var guestHandObject = GameObject.Find("GuestHandPos");
        _turnPhase = FindObjectOfType<TurnPhase>();
        _onlineGameManager = FindObjectOfType<OnlineGameManager>();

        if (masterObject != null)
            _masterSelectPosition = masterObject.GetComponent<SelectPosition>();

        if (masterHandObject != null)
            _masterHandPosition = masterHandObject.GetComponent<HandPosition>();
        
        if (guestObject != null)
            _guestSelectPosition = guestObject.GetComponent<SelectPosition>();

        if (guestHandObject != null)
            _guestHandPosition = guestHandObject.GetComponent<HandPosition>();
    }

    /// <summary>CharacterがClickされたときの処理</summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_onlineGameManager.Button.interactable) return; //ターンプレイヤーのみが選択できる
        
        //BattlePhaseに移行していたら
        if (_turnPhase.CurrentPhase == Phase.BattlePhase)
        {
            SelectBattle(this);
            Debug.Log("Battle開始");
        }

        var selectPosition = PhotonNetwork.IsMasterClient ? _masterSelectPosition : _guestSelectPosition;

        if (selectPosition == null ||
            selectPosition.SelectCard == null || _turnPhase.CurrentPhase != Phase.MainPhase) return;
        
        PowerValue += selectPosition.SelectCard.Power; //カードのPower分上昇
        selectPosition.DestroyCard();　//カードを削除
        photonView.RPC(nameof(OnRemoveCard), RpcTarget.Others, PhotonNetwork.IsMasterClient);
        CheckBattlePhase();　
    }

    /// <summary>手札のカードを使用したことを通知</summary>
    [PunRPC]
    public void OnRemoveCard(bool isMaster)
    {
        var handPos = isMaster ? _masterHandPosition : _guestHandPosition;
        if (handPos != null && !handPos.IsEmpty(isMaster))
        {
            var cardToRemove = handPos.GetRandomCard(isMaster);
            Debug.Log(cardToRemove);
            if (cardToRemove != null)
            {
                handPos.RemoveCard(cardToRemove, isMaster);
                Destroy(cardToRemove.gameObject);
            }
        }
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
            Debug.Log("Master transitioning to Battle Phase");
        }
        else
        {
            // ゲストクライアントのフェーズをバトルフェーズに変更
            _turnPhase.CurrentPhase = Phase.BattlePhase;
            _turnPhase.ActiveBattlePhase();
            Debug.Log("Guest transitioning to Battle Phase");
        }
    }

    //選択したキャラクターをリストに格納
    private void SelectBattle(ClickSelectCharacter character)
    {
        if (_turnPhase.CurrentPhase != Phase.BattlePhase) return;
        _turnPhase.Select(character);
    }

    public void OnCardSelected(Card card)
    {
    }
}

// if (OnlineGameManager.Instance._turn == Phase.BattlePhase)
// {
//     _battle.SelectCharacter(this);
// }