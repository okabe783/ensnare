using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>Characterが押された際の処理</summary>
public class ClickSelectCharacter : MonoBehaviour, IPointerClickHandler
{
    private SelectedCard _checkCard;
    public int _powerValue;　//攻撃力

    public void Update()
    {
        if (_checkCard == null)
            _checkCard = FindObjectOfType<SelectedCard>(); //特定のクラスが存在するobjectを全て取得する
    }

    /// <summary>CharacterがClickされたときの処理</summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_checkCard._selectCard != null)
        {
            _powerValue += _checkCard._selectCard.Power;　//CharacterにCardの攻撃力を加算
            _checkCard.Remove();　//使用したCardを削除
        }
    }
}