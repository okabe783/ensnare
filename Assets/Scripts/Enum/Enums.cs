namespace Ensnare.Enums //nameSpaceを記載してUsingで使用できるようにする
{
    public enum Phase
    {
        none,
        StartPhase,
        AfterSecondStart,
        MainPhase,
        BattlePhase,
        EndPhase,
    }

    public enum CardType
    {
        CharacterCard,
        TrapCard,
        DeathCard
    }

    public enum Result
    {
        Win,
        Lose,
    }
}