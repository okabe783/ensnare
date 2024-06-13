using UnityEngine;

/// <summary>
/// Cardを生成する為のscript
/// GeneratorObjectにアタッチする
/// </summary>
public class CardGenerator : MonoBehaviour
{
    [SerializeField] private HandPosition _handPos;
    public CardDataBase[] _playerCardDataBases;
    public Card _card;
    //public bool _isSelected { get; private set; }

    /// <summary>Cardを生成</summary>
    public Card CardSpawn(int cardNumber)
    {
        var card = Instantiate(_card);
        //CardがPlayer用なら
        card.CardSet(_playerCardDataBases[cardNumber]);
        AddHand(card);
        return card;
    }

    /// <summary>手札にCardを追加</summary>
    private void AddHand(Card card)
    {
        _handPos.Add(card);
        //card.OnClickCard = SelectCard;
    }

    public void ResetPosition()
    {
        _handPos.ResetPosition();
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