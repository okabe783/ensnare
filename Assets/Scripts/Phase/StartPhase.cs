using UnityEngine;
using Ensnare.Enums;

public class StartPhase : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private PanelSetUp panelSetUp;

    /// <summary>StartPhaseを実行</summary>
    public void StartPhaseSetUp(ref Phase currentPhase) //参照渡し
    {
        panelSetUp.BeginPhase(_startPanel);
        currentPhase = Phase.RefreshPhase;
    }
}
