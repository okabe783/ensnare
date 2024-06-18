using Ensnare.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>Characterが押された際の処理</summary>
public class ClickSelectCharacter : MonoBehaviour, IPointerClickHandler
{
    private SelectedCard _checkCard;
    public int _powerValue { get; set; }

    public void Update()
    {
        if (_checkCard == null)
            _checkCard = FindObjectOfType<SelectedCard>(); //特定のクラスが存在するobjectを全て取得する
    }

    /// <summary>CharacterがClickされたときの処理</summary>
    public void OnPointerClick(PointerEventData eventData)
    {
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
    }
}