using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    public Card _card;
    public Transform _destination;

    public void OnButtonClicked()
    {
        if (_card != null && _destination != null)
        {
            _card.transform.position = _destination.position;
        }
    }
}
