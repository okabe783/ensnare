namespace Ensnare.Enums //nameSpaceを記載してUsingで使用できるようにする
{
    public enum Turn
    {
        FirstTurn,
        SecondTurn
    }

    public enum Phase
    {
        none,
        StartPhase,
        AfterSecondStart,
        MainPhase,
        BattlePhase,
        EndPhase,
    }
}