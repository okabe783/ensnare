using Ensnare.Enums;
using UnityEngine;

public class RefreshPhase : MonoBehaviour
{
    [SerializeField] private GameObject _refreshPanel;
    [SerializeField] private PanelSetUp panelSetUp;
    [SerializeField] private PhotonObjectManager _photonObjectManager;
    [SerializeField] private HandPosition _handPosition;
    [SerializeField] private CreateCard _createCard;
    
    /// <summary>自分のターン開始時playerの情報をresetする</summary>
    //refは直接参照を渡すのでわずかに戻り値を返すよりも軽い(メソッドからの戻り値を返すときに新しい値を作るため)
    public void RefreshPhaseSetUp(ref Phase currentPhase)
    {
        foreach (var character in _photonObjectManager._masterCharacterList)
        {
            character._powerValue = 0;
        }

        foreach (var character in _photonObjectManager._guestCharacterList)
        {
            character._powerValue = 0;
        }
        
        panelSetUp.BeginPhase(_refreshPanel);
        currentPhase = Phase.MainPhase;
    }
    
    /// <summary>2ターン目以降のスタート時に呼び出される</summary>
    private void SetUpNextTurn(ref Phase currentPhase)
    {
        _handPosition.ResetCard();
        _createCard.DrawCard();
        currentPhase = Phase.MainPhase;
    }
}
