using UnityEngine;

/// <summary>ReaderCharacterと手札に関するクラス</summary>
public class Player : MonoBehaviour
{
    [SerializeField] private HandPosition _hand;
    [SerializeField] private SelectedCard _selectedCard;

    public HandPosition Hand => _hand;
    public bool _isSelected { get; private set; }
    
    /// <summary>選択されたときにカードを移動</summary>
    public void SetCardToHand(Card card)
    {
        Hand.Add(card); //playerの手札に追加
        card.OnClickCard = SelectCard;
    }
    
    /// <summary>手札のcardが押されたときcardを移動</summary>
    private void SelectCard(Card card)
    {
        if (_isSelected)
        {
            return;
        }

        //すでにセットされていれば手札に戻す
        if (_selectedCard._isSelectCard)
        {
            _hand.Add(_selectedCard._isSelectCard);
        }
        _hand.Remove(card);
        _selectedCard.Set(card);
        _hand.ResetPosition();
    }
}