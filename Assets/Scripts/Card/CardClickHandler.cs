using UnityEngine;
using UnityEngine.EventSystems;

public class CardClickHandler : MonoBehaviour,IPointerClickHandler
{
    private CardSelector _cardSelector;
    private Card card;

    private void Start()
    {
        _cardSelector = FindObjectOfType<CardSelector>();
        card = GetComponent<Card>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_cardSelector != null && card != null)
        {
            _cardSelector.NotifyCardSelected(card);
        }
    }
}
