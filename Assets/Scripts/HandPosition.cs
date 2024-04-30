using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    private List<Card> cardList = new List<Card>();

    //親をPlayerのHandPositionにしてCardをその子要素に設置
    public void Add(Card card)
    {
        cardList.Add(card);
        card.transform.SetParent(this.transform);
    }

    public void Remove(Card card)
    {
        cardList.Remove(card);
    }

    public void ResetPosition()
    {
        for (var i = 0; i < cardList.Count; i++)
        {
            var posX = i * 2f; //整列
            cardList[i].transform.localPosition = new Vector3(posX, 0);
        }
    }
}
