using System.Collections;
using Ensnare.Enums;
using UnityEngine;
using UnityEngine.UI;

public class StartPhase : PanelSetUp
{
    [SerializeField] private TurnPhase _turnPhase;
    [SerializeField] private GameObject _startPanel; 
    [SerializeField] private Text _phaseText;
    [SerializeField] private GameObjectManager gameObjectManager;
    [SerializeField] private HandPosition _handPosition;

    /// <summary>StartPhaseを実行</summary>
    public void StartPhaseSetUp()
    {
        Debug.Log("StartPhase");
        _phaseText.text = "Start Phase";
        BeginPhase(_startPanel);
        RefreshPhaseSetUp();
    }
    
    /// <summary>自分のターン開始時playerの情報をresetする</summary>
    //refは直接参照を渡すのでわずかに戻り値を返すよりも軽い(メソッドからの戻り値を返すときに新しい値を作るため)
    private void RefreshPhaseSetUp()
    {
        Debug.Log("RefreshPhase");
        foreach (var character in gameObjectManager.MasterCharacterList)
        {
            character._powerValue = 0;
        }

        foreach (var character in gameObjectManager.GuestCharacterList)
        {
            character._powerValue = 0;
        }

        _turnPhase.CurrentPhase = Phase.MainPhase;
    }
    
    /// <summary>2ターン目以降のスタート時に呼び出される</summary>
    public void SetUpNextTurn()
    {
        //_handPosition.ResetCard();
    }
}