using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>Characterが押された際の処理</summary>
public class ClickSelectCharacter : MonoBehaviour, IPointerClickHandler,IClick
{
    public int _powerValue { get; set; }
    private CardSelector _cardSelector;

    private void Start()
    {
        _cardSelector = FindObjectOfType<CardSelector>();
        if(_cardSelector != null)
            _cardSelector.AddObserver(this);
    }

    /// <summary>CharacterがClickされたときの処理</summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        var selectPosition = FindObjectOfType<SelectPosition>();
        if (selectPosition != null && selectPosition.SelectCard != null)
        {
            _powerValue += selectPosition.SelectCard.Power;
            selectPosition.DestroyCard();
        }
    }
    
    public void OnCardSelected(Card card)
    {
        
    }
    //
    // private void OnDestroy()
    // {
    //     if (_cardSelector != null)
    //     {
    //         _cardSelector.RemoveObserver(this);
    //     }
    // }
}
// if (_checkCard._isSelectCard != null && OnlineGameManager.Instance._turn == Phase.MainPhase)
// {
//     _powerValue += _checkCard._isSelectCard.Power;　//CharacterにCardの攻撃力を加算
//     _checkCard.DestroyCard();　//使用したCardを削除
// }
//
// if (OnlineGameManager.Instance._turn == Phase.BattlePhase)
// {
//     _battle.SelectCharacter(this);
// }