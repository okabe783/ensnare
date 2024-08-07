using System;
using Ensnare.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// CardPrefabにアタッチする
/// ScriptableObjectのデータを読み込む
/// </summary>
public class Card : MonoBehaviour
{
    public UnityAction<Card> OnClickCard; //CardがClickされたときに実行されるアニメーションイベント

    //Card情報
    [SerializeField] private Image _icon;
    [SerializeField] private Text _powerText;
    [SerializeField] private int _power;

    public int Power => _power;
    public bool IsPlayer { get; set; }

    public CardType CardType { get; private set; }

    //ScriptableObjectで設定したCard情報を読み込む
    public void CardSet(CardDataBase cardDataBase)
    {
        _icon.sprite = cardDataBase.Icon;
        _powerText.text = cardDataBase.PowerText;
        _power = cardDataBase.Power;
        CardType = cardDataBase.Type;
    }

    /// <summary>CardがClickされたことを通知する</summary>
    public void OnClick()
    {
        OnClickCard?.Invoke(this);　//自分を登録
    }
}