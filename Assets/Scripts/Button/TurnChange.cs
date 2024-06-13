using UnityEngine;
using Ensnare.Enums;

public class TurnChange : MonoBehaviour,IButton
{
    public Turn CurrentTurn { get; private set; }

    public void OnClickGuestTurn()
    {
        CurrentTurn = Turn.GuestTurn;
        Debug.Log(CurrentTurn);
    }

    public void OnClickMasterTurn()
    {
        CurrentTurn = Turn.MasterTurn;
        Debug.Log(CurrentTurn);
    }
}