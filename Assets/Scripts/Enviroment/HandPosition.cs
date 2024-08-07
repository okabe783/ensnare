using System.Collections.Generic;
using UnityEngine;

/// <summary>手札となる場所にアタッチする</summary>
public class HandPosition : MonoBehaviour
{
    private List<Card> _masterCardList = new();
    private List<Card> _guestCardList = new();

    //親をPlayerのHandPositionにしてCardをその子要素に設置
    public void Add(Card card, bool isPlayer)
    {
        if (isPlayer)
        {
            _masterCardList.Add(card);
            card.transform.SetParent(transform);
        }
        else
        {
            _guestCardList.Add(card);
            card.transform.SetParent(transform); 
        }

        ResetHandPosition(isPlayer);
    }

    /// <summary>Cardの削除</summary>
    public void RemoveCard(Card card, bool isPlayer)
    {
        if (isPlayer)
        {
            _masterCardList.Remove(card);
        }
        else
        {
            _guestCardList.Remove(card);
        }
    }

    /// <summary>手札の場所を調整</summary>
    public void ResetHandPosition(bool isPlayer)
    {
        var cardList = isPlayer ? _masterCardList : _guestCardList;
        for (var i = 0; i < cardList.Count; i++)
        {
            var posZ = i * (isPlayer ? 1.8f : 1.3f);
            cardList[i].transform.localPosition = new Vector3(0, 0, posZ); //手札の場所を指定
        }
    }

    //手札が空かどうかの判定
    public bool IsEmpty(bool isPlayer)
    {
        return isPlayer ? _masterCardList.Count == 0 : _guestCardList.Count == 0;
    }

    // 特定のクライアントの手札からランダムにカードを取得
    public Card GetRandomCard(bool isMaster)
    {
        var cardList = isMaster ? _masterCardList : _guestCardList;
        var randomIndex = Random.Range(0, cardList.Count);
        return cardList[randomIndex];
    }

    //カードの削除
    public void ResetCard(bool isPlayer)
    {
        var cardList = isPlayer ? _masterCardList : _guestCardList;
        foreach (var card in _masterCardList)
        {
            Destroy(card.gameObject);
        }

        cardList.Clear();
    }
}