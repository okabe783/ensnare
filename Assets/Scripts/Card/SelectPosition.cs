using UnityEngine;

/// <summary>Clickされたカードが置かれる場所</summary>
public class SelectPosition : MonoBehaviour,IClick
{
    public Card SelectCard { get; private set; } //選択されたカードを管理
    private CardSelector _cardSelector; //監視クラス

    private void Start()
    {
        _cardSelector = FindObjectOfType<CardSelector>(); 

        if (_cardSelector == null) return;
        
        _cardSelector.AddObserver(this);　//通知を受け取る側として追加する
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
        Destroy(SelectCard.gameObject);
        SelectCard = null;
    }

    /// <summary>通知がきたら</summary>
    public void OnCardSelected(Card card)
    {
        // ここでカードをセットする
        if (_cardSelector != null)
            _cardSelector.SetChoiceCard(card,card);
    }
}
