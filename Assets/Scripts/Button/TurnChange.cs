using UnityEngine;
using Ensnare.Enums;

public class TurnChange : MonoBehaviour,IButton
{
    [SerializeField] private TurnPhase _turnPhase;
    public Turn CurrentTurn { get; private set; }

    public void GuestTurn()
    {
        CurrentTurn = Turn.GuestTurn;
        _turnPhase.CurrentPhase = Phase.StartPhase;
    }

    public void MasterTurn()
    {
        CurrentTurn = Turn.MasterTurn;
        _turnPhase.CurrentPhase = Phase.StartPhase;
    }
}