using UnityEngine;

/// <summary>CardPrefabにアタッチする</summary>
public class Card : MonoBehaviour
{
    public CardDataBase _cardDataBase { get; private set; }
    //CardUI
    private Sprite _icon;
    private string _description;
    
    //ScriptableObjectで設定したCardを読み込む
    public void CardSet(CardDataBase cardDataBase)
    {
        _cardDataBase = cardDataBase;
        _icon = _cardDataBase.Icon;
        _description = _cardDataBase.Description;
    }
}
