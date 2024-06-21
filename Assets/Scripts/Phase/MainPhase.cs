using UnityEngine;
using UnityEngine.UI;

public class MainPhase : PanelSetUp
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private Text _phaseText;

    /// <summary>MainPhasePanelをセット</summary>
    public void MainPhaseSetUp()
    {
        _phaseText.text = "Main Phase";
        BeginPhase(_mainPanel);
        Debug.Log("MainPhaseに移行");
    }
}
