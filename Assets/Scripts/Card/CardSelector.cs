using System.Collections.Generic;
using UnityEngine;

/// <summary>Cardの選択を監視するクラス</summary>
public class CardSelector : MonoBehaviour
{
    private readonly List<IClick> observers = new();

    public SelectPosition SelectPosition { get; set; }
    [field: SerializeField] public HandPosition HandPosition { get; set; }

    //CardがClickされたときに通知を受け取る
    public void AddObserver(IClick observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IClick observer)
    {
        observers.Remove(observer);
    }

    public void NotifyCardSelected(Card card)
    {
        foreach (var observer in observers)
        {
            observer.OnCardSelected(card);
        }
    }
    
    //手札のカードを選択されたときに提出位置にセット
    public void SetChoiceCard(Card card)
    {
        //すでにセットしていれば、手札に戻す
        if (SelectPosition.SelectCard)
        {
            HandPosition.Add(SelectPosition.SelectCard);
        }
        HandPosition.RemoveCard(card);
        SelectPosition.Set(card);
        HandPosition.ResetHandPosition();
    }
}
