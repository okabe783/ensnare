using UnityEngine;

/// <summary>選択されたCardに関するScript</summary>
public class SelectedCard : MonoBehaviour
{
    //選択されたCardを管理
    public Card _isSelectCard { get; private set; }
    
    //自分の子要素にして位置を自分の位置に合わせる
    public void Set(Card card)
    {
        _isSelectCard = card;
        card.transform.SetParent(transform);
        card.transform.position = transform.position;
    }

    //Cardの削除
    public void DestroyCard()
    {
        if (_isSelectCard != null)
        {
            Destroy(_isSelectCard.gameObject);
        }
    }
}