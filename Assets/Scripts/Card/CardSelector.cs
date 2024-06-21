using System.Collections.Generic;
using UnityEngine;

/// <summary>Cardの選択を監視するクラス</summary>
public class CardSelector : MonoBehaviour
{
    private readonly List<IClick> observers = new(); //通知したい場所を追加するためのリスト
    public SelectPosition SelectPosition { get; set; }
    [field: SerializeField] public HandPosition HandPosition { get; set; }

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
