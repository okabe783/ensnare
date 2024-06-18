using UnityEngine;

public class EndPhase : MonoBehaviour
{
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private PanelSetUp panelSetUp;
    
    /// <summary>EndPhasePanelをセット</summary>
    public void EndPhaseSetUp()
    {
        panelSetUp.BeginPhase(_endPanel);
    }
}
