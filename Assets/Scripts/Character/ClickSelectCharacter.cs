using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSelectCharacter : MonoBehaviour,IPointerClickHandler
{
    private SelectedCard selectCard;

    public void Update()
    {
        if (selectCard == null)
        {
            selectCard = FindObjectOfType<SelectedCard>(); //特定のクラスが存在するobjectを全て取得する
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectCard._selectCard != null)
        {
            Debug.Log("characterがClickされました");
        }
    }
}