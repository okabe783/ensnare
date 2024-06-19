using UnityEngine;

/// <summary>Clickされたカードが置かれる場所</summary>
public class SelectPosition : MonoBehaviour,IClick
{
    public Card SelectCard { get; private set; } //選択されたカードを管理

    private void Start()
    {
        var cardSelector = FindObjectOfType<CardSelector>();

        if (cardSelector != null)
        {
            cardSelector.AddObserver(this);
            cardSelector.SelectPosition = this;
        }
    }
    
    //自分の子要素にする。位置を合わせる
    public void Set(Card card)
    {
        SelectCard = card;
        card.transform.SetParent(transform);
        card.transform.position = transform.position;
    }

    public void DestroyCard()
    {
        if (SelectCard != null)
        {
            Destroy(SelectCard.gameObject);
            SelectCard = null;
        }
    }

    public void OnCardSelected(Card card)
    {
        // ここでカードをセットする
        var cardSelector = FindObjectOfType<CardSelector>();
        if (cardSelector != null)
        {
            cardSelector.SetChoiceCard(card);
        }
    }
}
