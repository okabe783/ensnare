using Ensnare.Enums;
using UnityEngine;

/// <summary>Cardのデータを管理するScriptableObject</summary>
[CreateAssetMenu(menuName = "Scriptable /Create Card")]
public class CardDataBase : ScriptableObject
{
    [SerializeField] private string _name;　
    [SerializeField] private int _cardId;
    [SerializeField] private int _power;
    [SerializeField] private CardType _cardType;
    [SerializeField] private Sprite _iconImage;
    [SerializeField] private string _powerText;

    public string Name => _name;
    public int CardId => _cardId;
    public int Power => _power;
    public CardType Type => _cardType;
    public Sprite Icon => _iconImage;
    public string PowerText => _powerText;
}