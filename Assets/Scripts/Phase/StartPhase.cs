using UnityEngine;
using Ensnare.Enums;
using UnityEngine.Serialization;

public class StartPhase : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private PanelSetUp panelSetUp;
    [FormerlySerializedAs("_photonObjectManager")] [SerializeField] private GameObjectManager gameObjectManager;
    [SerializeField] private HandPosition _handPosition;

    /// <summary>StartPhaseを実行</summary>
    public void StartPhaseSetUp()
    {
        panelSetUp.BeginPhase(_startPanel);
        RefreshPhaseSetUp();
        
    }
    
    /// <summary>自分のターン開始時playerの情報をresetする</summary>
    //refは直接参照を渡すのでわずかに戻り値を返すよりも軽い(メソッドからの戻り値を返すときに新しい値を作るため)
    private void RefreshPhaseSetUp()
    {
        foreach (var character in gameObjectManager.MasterCharacterList)
        {
            character._powerValue = 0;
        }

        foreach (var character in gameObjectManager.GuestCharacterList)
        {
            character._powerValue = 0;
        }
    }
    
    /// <summary>2ターン目以降のスタート時に呼び出される</summary>
    public void SetUpNextTurn()
    {
        _handPosition.ResetCard();
        //_createCard.DrawCard();
    }
}