using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSelectCharacter : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("characterがClickされました");
    }
}