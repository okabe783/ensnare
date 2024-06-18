using UnityEngine;

/// <summary>Cardを生成する為のscript</summary>
public class CardGenerator : MonoBehaviour
{
    [SerializeField,Header("手札を管理するPosition")] private HandPosition _handPos;
    [SerializeField] private CardDataBase[] _playerCardDataBases;
    [SerializeField] private Card _card;

    /// <summary>Cardを生成</summary>
    public void CardSpawn(int cardNumber)
    {
        var card = Instantiate(_card); 
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

//     /// <summary>手札のcardが押されたときcardを移動</summary>
//     // private void SelectCard(Card card)
//     // {
//     //     if (_isSelected) return;
//     //
//     //     //すでにセットされていれば手札に戻す
//     //     if (_selectedCard._isSelectCard)
//     //     {
//     //         _hand.Add(_selectedCard._isSelectCard);
//     //     }
//     //
//     //     _hand.Remove(card);
//     //     _selectedCard.Set(card);
//     //     _hand.ResetPosition();
//     // }
// }