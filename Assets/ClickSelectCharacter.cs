using Ensnare.Enums;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>Characterが押された際の処理</summary>
public class ClickSelectCharacter : MonoBehaviourPunCallbacks, IPointerClickHandler, IClick
{
    private TurnPhase _turnPhase;

    public int PowerValue { get; set; }

    //手札のPosition
    private HandPosition _masterHandPosition;
    private HandPosition _guestHandPosition;

    //Clickしたカードを移動させるエリア
    private SelectPosition _masterSelectPosition;
    private SelectPosition _guestSelectPosition;

    private void Start()
    {
        var masterObject = GameObject.Find("ChoiceMasterCardArea");
        var masterHandObject = GameObject.Find("MasterHandPos");
        
        if (masterObject != null)
        {
            _masterSelectPosition = masterObject.GetComponent<SelectPosition>();
        }

        if (masterHandObject != null)
        {
            _masterHandPosition = masterHandObject.GetComponent<HandPosition>();
        }

        var guestObject = GameObject.Find("GuestSelectPositionObjectName");
        var guestHandObject = GameObject.Find("GuestHandPos");
        if (guestObject != null)
        {
            _guestSelectPosition = guestObject.GetComponent<SelectPosition>();
        }

        if (guestHandObject != null)
        {
            _guestHandPosition = guestHandObject.GetComponent<HandPosition>();
        }

        _turnPhase = FindObjectOfType<TurnPhase>();
    }

    /// <summary>CharacterがClickされたときの処理</summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_turnPhase.CurrentPhase == Phase.BattlePhase)
        {
            SelectBattle(this);
        }
        var selectPosition = PhotonNetwork.IsMasterClient ? _masterSelectPosition : _guestSelectPosition;
        if (selectPosition != null &&
            (selectPosition.SelectCard != null || _turnPhase.CurrentPhase == Phase.MainPhase))
        {
            PowerValue += selectPosition.SelectCard.Power; //カードのPowerを上昇
            selectPosition.DestroyCard();
            photonView.RPC(nameof(OnRemoveCard), RpcTarget.Others, PhotonNetwork.IsMasterClient);
            CheckBattlePhase();
        }
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
        if (isMaster)
        {
            // マスタークライアントのフェーズをバトルフェーズに変更
            _turnPhase.CurrentPhase = Phase.BattlePhase;
            Debug.Log("Master transitioning to Battle Phase");
        }
        else
        {
            // ゲストクライアントのフェーズをバトルフェーズに変更
            _turnPhase.CurrentPhase = Phase.BattlePhase;
            Debug.Log("Guest transitioning to Battle Phase");
        }
    }

    //選択したキャラクターをリストに格納
    private void SelectBattle(ClickSelectCharacter character)
    {
        if (_turnPhase.CurrentPhase != Phase.BattlePhase) return;
        _turnPhase.Select(character);
        Debug.Log(character);
    }

    public void OnCardSelected(Card card)
    {
    }
}

// if (OnlineGameManager.Instance._turn == Phase.BattlePhase)
// {
//     _battle.SelectCharacter(this);
// }