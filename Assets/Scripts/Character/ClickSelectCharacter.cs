using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSelectCharacter : MonoBehaviour, IPointerClickHandler
{
    private SelectedCard _checkCard;
    public int _powerValue;

    public void Update()
    {
        if (_checkCard == null)
            _checkCard = FindObjectOfType<SelectedCard>(); //特定のクラスが存在するobjectを全て取得する
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_checkCard._selectCard != null)
        {
            _powerValue += _checkCard._selectCard.Power;
            _checkCard.Remove();
        }
    }
}