using Ensnare.Enums;
using UnityEngine;

public class MainPhase : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private PanelSetUp panelSetUp;

    /// <summary>MainPhasePanelをセット</summary>
    public void MainPhaseSetUp()
    {
        panelSetUp.BeginPhase(_mainPanel);
    }

    /// <summary>MainからBattleに移行 </summary>
    /// <param name="currentPhase"></param>
    public void ChangeTurn(ref Phase currentPhase)
    {
        currentPhase = Phase.BattlePhase;
    }
}
