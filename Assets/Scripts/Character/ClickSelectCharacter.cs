using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSelectCharacter : MonoBehaviour, IPointerClickHandler
{
    private SelectedCard _selectCard;
    public int _powerValue;
    
    public void Update()
    {
        if (_selectCard == null)
            _selectCard = FindObjectOfType<SelectedCard>(); //特定のクラスが存在するobjectを全て取得する
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_selectCard._selectCard != null)
        {
            _powerValue += _selectCard._selectCard.Power;
            Debug.Log(_powerValue);
        }
    }
}