namespace Ensnare.Enums //nameSpaceを記載してUsingで使用できるようにする
{
    public enum Turn
    {
        MasterTurn,
        GuestTurn
    }

    public enum Phase
    {
        StartPhase,
        RefreshPhase,
        MainPhase,
        BattlePhase,
        EndPhase,
    }
}