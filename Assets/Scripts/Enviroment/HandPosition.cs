using System.Collections.Generic;
using UnityEngine;

/// <summary>手札となる場所にアタッチする</summary>
public class HandPosition : MonoBehaviour
{
    private List<Card> cardList = new List<Card>();

    //親をPlayerのHandPositionにしてCardをその子要素に設置
    public void Add(Card card)
    {
        cardList.Add(card);
        card.transform.SetParent(this.transform);
    }

    /// <summary>Cardの削除</summary>
    public void Remove(Card card)
    {
        cardList.Remove(card);
    }
    
    /// <summary>手札の場所を調整</summary>
    public void ResetPosition()
    {
        for (var i = 0; i < cardList.Count; i++)
        {
            var posZ = i * 2f; 
            //手札の場所を指定
            cardList[i].transform.localPosition = new Vector3(0, 0,posZ);
        }
    }

    //手札が空かどうかの判定
    public bool IsEmpty()
    {
        return cardList.Count == 0;
    }
}