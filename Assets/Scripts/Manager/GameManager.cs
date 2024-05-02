using UnityEngine;
using static CardGenerator;

public class GameManager : MonoBehaviour
{
    public GameObject _mainPhase;

    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private HandPosition _handPosition;
    [SerializeField] private SelectedCard _selectedCard;
    
    public bool _isSelected { get; private set; }

    private void Awake()
    {
        _mainPhase.SetActive(false);
    }

    private void Start()
    {
        DrawFirstHand();
    }

    private void DrawFirstHand()
    {
        for (var i = 0; i < 6; i++)
        {
            var card = _cardGenerator.CardSpawn(CardType.Player); //Cardを配る
            SetCardToHand(card);
        }
        _handPosition.ResetPosition();
    }

    private void SetCardToHand(Card card)
    {
        _handPosition.Add(card); //playerの手札に追加
        card.OnClickCard = SelectCard;
    }

    private void SelectCard(Card card)
    {
        if (_isSelected)
        {
            return;
        }

        if (_selectedCard._selectCard)
        {
            _handPosition.Add(_selectedCard._selectCard);
        }
        _selectedCard.Set(card);
    }
}