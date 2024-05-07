using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>CardPrefabにアタッチする</summary>
public class Card : MonoBehaviour
{
    public CardDataBase _cardDataBase { get; private set; }

    public UnityAction<Card> OnClickCard; //CardがClickされたときに実行

    //CardUI
    [SerializeField] private Image _icon;
    [SerializeField] private Text _powerText;
    [SerializeField] private int _power;

    public int Power => _power;

    //ScriptableObjectで設定したCardを読み込む
    public void CardSet(CardDataBase cardDataBase)
    {
        _cardDataBase = cardDataBase;
        _icon.sprite = cardDataBase.Icon;
        _powerText.text = cardDataBase.PowerText;
        _power = cardDataBase.Power;
    }

    /// <summary>CardがClickされたことを通知する</summary>
    public void OnClick()
    {
        OnClickCard?.Invoke(this);
    }
}