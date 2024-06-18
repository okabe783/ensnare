using System;
using UnityEngine;

/// <summary>選択されたCardに関するScript</summary>
public class SelectedCard : MonoBehaviour
{
    private Transform _choiceCardArea;
    private HandPosition _handPosition;

    private void Start()
    {
        _handPosition = FindObjectOfType<HandPosition>();
        _choiceCardArea = GameObject.Find("ChoiceCardArea").transform;
        
        // カードがクリックされたときに SetChoiceCard を実行する
        var card = GetComponent<Card>(); // この例では同じオブジェクトにアタッチされている Card コンポーネントを取得する
        if (card != null)
        {
            card.OnClickCard += SetChoiceCard;
        }
    }
    private void SetChoiceCard(Card selectCard)
    {
        // すでに移動先にカードが存在している場合
        if (_choiceCardArea.childCount > 0)
        {
            // 先頭のカードを手札に戻す
            var existingCard = _choiceCardArea.GetChild(0).GetComponent<Card>();
            _handPosition.Add(existingCard);
            Destroy(existingCard.gameObject);
        }

        // 選択されたカードを移動先に配置する
        selectCard.transform.SetParent(_choiceCardArea);
        selectCard.transform.localPosition = Vector3.zero; // 移動先の中心に配置する

        // 選択されたカードを手札から削除する
        _handPosition.RemoveCard(selectCard);
    }
}
    