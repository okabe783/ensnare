using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

/// <summary>Cardの選択を監視するクラス</summary>
public class CardSelector : MonoBehaviour
{
    private readonly List<IClick> observers = new(); //通知したい場所を追加するためのリスト
    [SerializeField] private SelectPosition _selectMasterPosition;
    [SerializeField] private SelectPosition _selectGuestPosition;
    [field: SerializeField] public HandPosition MasterHandPosition { get; set; }
    [field: SerializeField] public HandPosition GuestHandPosition { get; set; }

    /// <summary>CardがClickされたときに通知を受け取る</summary>
    public void AddObserver(IClick observer)
    {
        observers.Add(observer);
    }

    /// <summary>Cardが選択されたことを通知</summary>
    public void NotifyCardSelected(Card card)
    {
        //登録されている場所に通知を送る
        foreach (var observer in observers)
        {
            observer.OnCardSelected(card);
        }
    }

    /// <summary>手札のカードを選択されたときに提出位置にセット</summary>
    public void SetChoiceCard(Card card, bool isPlayer)
    {
        var handPos = PhotonNetwork.IsMasterClient ? MasterHandPosition : GuestHandPosition;
        var selectPos = PhotonNetwork.IsMasterClient ? _selectMasterPosition : _selectGuestPosition;

        // すでにセットしていれば、手札に戻す
        if (selectPos.SelectCard != null)
        {
            var setHandPosition = selectPos.SelectCard.IsPlayer ? MasterHandPosition : GuestHandPosition;
            Debug.Log(selectPos.SelectCard.IsPlayer);
            setHandPosition.Add(selectPos.SelectCard, selectPos.SelectCard.IsPlayer);
        }

        handPos.RemoveCard(card, PhotonNetwork.IsMasterClient);
        selectPos.Set(card);
        handPos.ResetHandPosition(PhotonNetwork.IsMasterClient);
    }
}