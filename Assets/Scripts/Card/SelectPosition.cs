using UnityEngine;

/// <summary>Clickされたカードが置かれる場所</summary>
public class SelectPosition : MonoBehaviour,IClick
{
    public Card SelectCard { get; private set; } //選択されたカードを管理

    private void Start()
    {
        var cardSelector = FindObjectOfType<CardSelector>(); //監視クラスを追加

        if (cardSelector == null) return;
        
        cardSelector.AddObserver(this);　//通知を受け取る側として追加する
        cardSelector.SelectPosition = this;　
    }
    
    /// <summary>自分の子要素にする。位置を合わせる</summary>
    public void Set(Card card)
    {
        SelectCard = card;
        card.transform.SetParent(transform);
        card.transform.position = transform.position;
    }

    /// <summary>カードを削除</summary>
    public void DestroyCard()
    {
        if (SelectCard == null) return;
        Destroy(SelectCard.gameObject);
        SelectCard = null;
    }

    /// <summary>通知がきたら</summary>
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
