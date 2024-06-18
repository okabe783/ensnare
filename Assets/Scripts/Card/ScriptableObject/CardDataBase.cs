using UnityEngine;

/// <summary>CardのScriptableObject</summary>
[CreateAssetMenu(menuName = "Scriptable /Create Card")]
public class CardDataBase : ScriptableObject
{
    public string _name;　
    public int _cardId;　
    public int _power;
    public CardType _cardType;
    public Sprite iconImage;
    public string _powerText;

    public Sprite Icon => iconImage;
    public string PowerText => _powerText;
    public int Power => _power;

    public enum CardType
    {
        Weapon,
        Trap,
    }
}