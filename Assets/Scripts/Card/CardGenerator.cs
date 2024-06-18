using UnityEngine;

/// <summary>Cardを生成する為のscript</summary>
public class CardGenerator : MonoBehaviour
{
    [SerializeField,Header("手札を管理するPosition")] private HandPosition _handPos;
    [SerializeField] private CardDataBase[] _playerCardDataBases;
    [SerializeField] private Card _cardPrefab;
    

    /// <summary>Cardを生成</summary>
    public void CardSpawn(int cardNumber)
    {
        var card = Instantiate(_cardPrefab); 
        card.CardSet(_playerCardDataBases[cardNumber]);
        AddHand(card);
    }

    /// <summary>手札にCardを追加</summary>
    private void AddHand(Card card)
    {
        _handPos.Add(card);
    }

    //手札の位置を調整する
    public void ResetPosition()
    {
        _handPos.ResetHandPosition();
    }
}