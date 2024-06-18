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
}
