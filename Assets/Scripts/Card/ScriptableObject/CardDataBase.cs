using UnityEngine;

/// <summary>Cardのデータを管理するScriptableObject</summary>
[CreateAssetMenu(menuName = "Scriptable /Create Card")]
public class CardDataBase : ScriptableObject
{
    public string _name;　
    public int _cardId;
    public int _power;
    public CardType _cardType;
    public Sprite _iconImage;
    public string _powerText;

    public Sprite Icon => _iconImage;
    public string PowerText => _powerText;
    public int Power => _power;

    //カードの種類
    public enum CardType
    {
        none,
        Weapon,
        Trap,
    }
}