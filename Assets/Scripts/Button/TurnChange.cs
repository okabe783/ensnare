using UnityEngine;
using Ensnare.Enums; 

public class TurnChange : MonoBehaviour,IButton
{
    public Turn CurrentTurn { get; private set; }

    public void OnClickGuestTurn()
    {
        Debug.Log("GuestTurn");
        CurrentTurn = Turn.GuestTurn;
    }

    public void OnClickMasterTurn()
    {
        Debug.Log("MasterTurn");
        CurrentTurn = Turn.MasterTurn;
    }
}