using UnityEngine;
using UnityEngine.UI;

public class BattlePhase : PanelSetUp
{
    [SerializeField] private GameObject _battlePanel;
    [SerializeField] private Text _phaseText;
    [SerializeField] private GameObject _button;
    private readonly ClickSelectCharacter[] _selectCharacters = new ClickSelectCharacter[2];
    
    /// <summary>BattlePhasePanelをset</summary>
    public void BattlePhaseSetUp()
    {
        _phaseText.text = "Battle Phase";
        BeginPhase(_battlePanel);
        _button.SetActive(true);
    }
    
    /// <summary>Battleを実行する</summary>
    private void InitBattle()
    {
        if(_selectCharacters[0] == null || _selectCharacters[1] == null) return;

        var power1 = -_selectCharacters[0].PowerValue;
        var power2 = -_selectCharacters[1].PowerValue;
        
        Debug.Log(power1 >= power2 ? "1の勝ち" : "２の勝ち");
        
        // キャラクター選択をリセット
        _selectCharacters[0] = null;
        _selectCharacters[1] = null;
    }
    
    /// <summary>選択されたcharacterをセットしてBattleを開始する</summary>
    public void SelectCharacter(ClickSelectCharacter character)
    {
        if (_selectCharacters[0] == null)
        {
            _selectCharacters[0] = character;
            Debug.Log(_selectCharacters[1]);
        }
        else if (_selectCharacters[1] == null)
        {
            _selectCharacters[1] = character;
            InitBattle();　//characterが2体セットされたらBattleを開始する
        }
    }
}
