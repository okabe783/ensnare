using UnityEngine;

public class BattlePhase : MonoBehaviour
{
    [SerializeField] private GameObject _battlePanel;
    [SerializeField] private PanelSetUp panelSetUp;
    [SerializeField] private GameObject _button;
    
    /// <summary>BattlePhasePanelをset</summary>
    public void BattlePhaseSetUp()
    {
        panelSetUp.BeginPhase(_battlePanel);
        _button.SetActive(true);
    }
}
