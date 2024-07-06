using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPhase : PanelSetUp
{
    [SerializeField] private TurnPhase _turnPhase;
    [SerializeField] private GameObject _startPanel; 
    [SerializeField] private Text _phaseText;
    [SerializeField] private HandPosition _handPosition;
    [SerializeField] private List<CharacterValue> _characterValue;

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

        // ターンプレイヤーのキャラクターのPowerValueをリセットする
        // 全ての CharacterValue をリセットする
        foreach (var characterValue in _characterValue)
        {
            characterValue.ResetValue();
        }
    }
    
    /// <summary>2ターン目以降のスタート時に呼び出される</summary>
    public void SetUpNextTurn()
    {
        //_handPosition.ResetCard();
    }
}