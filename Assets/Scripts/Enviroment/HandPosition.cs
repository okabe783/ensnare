using System.Collections.Generic;
using UnityEngine;

/// <summary>手札となる場所にアタッチする</summary>
public class HandPosition : MonoBehaviour
{
    private readonly List<Card> _cardList = new();

    //親をPlayerのHandPositionにしてCardをその子要素に設置
    public void Add(Card card)
    {
        _cardList.Add(card);
        card.transform.SetParent(this.transform);
    }

    /// <summary>Cardの削除</summary>
    public void Remove(Card card)
    {
        _cardList.Remove(card);
    }
    
    /// <summary>手札の場所を調整</summary>
    public void ResetPosition()
    {
        for (var i = 0; i < _cardList.Count; i++)
        {
            var posZ = i * 1.8f; 
            //手札の場所を指定
            _cardList[i].transform.localPosition = new Vector3(0, 0,posZ);
        }
    }

    //手札が空かどうかの判定
    public bool IsEmpty()
    {
        return _cardList.Count == 0;
    }
    
    public void ResetCard()
    {
        foreach (var card in _cardList)
        {
            Destroy(card.gameObject);
        }
        _cardList.Clear();
    }
}