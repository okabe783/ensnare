using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

/// <summary>Cardの選択を監視するクラス</summary>
public class CardSelector : MonoBehaviourPun
{
    private readonly List<IClick> _observers = new(); //通知したい場所を追加するためのリスト

    [SerializeField, Header("マスターの選択したカードを配置する場所")]
    private SelectPosition _selectMasterPosition;

    [SerializeField, Header("ゲストの選択したカードを配置する場所")]
    private SelectPosition _selectGuestPosition;

    [field: SerializeField] public HandPosition MasterHandPosition { get; set; }
    [field: SerializeField] public HandPosition GuestHandPosition { get; set; }

    /// <summary>CardがClickされたときに通知を受け取る</summary>
    public void AddObserver(IClick observer)
    {
        _observers.Add(observer);
    }

    /// <summary>Cardが選択されたことを通知</summary>
    public void NotifyCardSelected(Card card)
    {
        //登録されている場所に通知を送る
        foreach (var observer in _observers)
        {
            observer.OnCardSelected(card);
        }
    }

    /// <summary>手札のカードを選択されたときに提出位置にセット</summary>
    public void SetChoiceCard(Card card, bool isPlayer)
    {
        //MasterかGuestかで位置を切り替える
        var handPos = PhotonNetwork.IsMasterClient ? MasterHandPosition : GuestHandPosition;
        var selectPos = PhotonNetwork.IsMasterClient ? _selectMasterPosition : _selectGuestPosition;
        
        // すでにセットしていれば、手札に戻す
        if (selectPos.SelectCard != null)
        {
            var setHandPosition = selectPos.SelectCard.IsPlayer ? MasterHandPosition : GuestHandPosition;
            setHandPosition.Add(selectPos.SelectCard, selectPos.SelectCard.IsPlayer);
        }

        //Guestならfalseに変更
        if (handPos == GuestHandPosition)
        {
            isPlayer = false;
        }

        handPos.RemoveCard(card, isPlayer);
        selectPos.Set(card);
        handPos.ResetHandPosition(isPlayer);
    }
}