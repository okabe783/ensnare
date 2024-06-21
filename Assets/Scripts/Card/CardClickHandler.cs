using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>カードがクリックされた時通知を飛ばす</summary>
public class CardClickHandler : MonoBehaviour,IPointerClickHandler
{
    private CardSelector _cardSelector;
    private Card card;

    private void Start()
    {
        _cardSelector = FindObjectOfType<CardSelector>();
        card = GetComponent<Card>();
    }
    
    /// <summary>通知を送る</summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_cardSelector != null && card != null)
        {
            _cardSelector.NotifyCardSelected(card);
        }
    }
}
