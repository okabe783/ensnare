public class TurnChange : IButton
{
    private enum Turn
    {
        MasterTurn,
        GuestTurn
    }

    private Turn _currentTurn;
    
    public void OnClickGuestTurn()
    {
        _currentTurn = Turn.GuestTurn;
    }

    public void OnClickMasterTurn()
    {
        _currentTurn = Turn.MasterTurn;
    }
}