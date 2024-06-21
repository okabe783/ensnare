using UnityEngine;

public class EndPhase : PanelSetUp
{
    [SerializeField] private GameObject _endPanel;
    
    /// <summary>EndPhasePanelをセット</summary>
    public void EndPhaseSetUp()
    {
        BeginPhase(_endPanel);
    }
}
