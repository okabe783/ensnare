using UnityEngine;
using Ensnare.Enums;

public class StartPhase : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private PanelSetUp panelSetUp;
    [SerializeField] private PhotonObjectManager _photonObjectManager;
    [SerializeField] private HandPosition _handPosition;
    [SerializeField] private CreateCard _createCard;

    /// <summary>StartPhaseを実行</summary>
    public void StartPhaseSetUp(ref Phase currentPhase) //参照渡し
    {
        panelSetUp.BeginPhase(_startPanel);
        currentPhase = Phase.MainPhase;
        RefreshPhaseSetUp();
    }
    
    /// <summary>自分のターン開始時playerの情報をresetする</summary>
    //refは直接参照を渡すのでわずかに戻り値を返すよりも軽い(メソッドからの戻り値を返すときに新しい値を作るため)
    private void RefreshPhaseSetUp()
    {
        foreach (var character in _photonObjectManager.MasterCharacterList)
        {
            character._powerValue = 0;
        }

        foreach (var character in _photonObjectManager.GuestCharacterList)
        {
            character._powerValue = 0;
        }
    }
    
    /// <summary>2ターン目以降のスタート時に呼び出される</summary>
    private void SetUpNextTurn(ref Phase currentPhase)
    {
        _handPosition.ResetCard();
        _createCard.DrawCard();
        currentPhase = Phase.MainPhase;
    }
}