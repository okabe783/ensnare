using UnityEngine;

public class Battle : MonoBehaviour
{
    private ClickSelectCharacter[] _selectCharacters = new ClickSelectCharacter[1];

    /// <summary>Battleを実行する</summary>
    private void InitBattle()
    {
        if(_selectCharacters[0] == null || _selectCharacters[1] == null) return;

        var power1 = -_selectCharacters[0]._powerValue;
        var power2 = -_selectCharacters[1]._powerValue;
        
        Debug.Log(power1 >= power2 ? "1の勝ち" : "２の勝ち");
    }
    /// <summary>選択されたcharacterをセットしてBattleを開始する</summary>
    public void SelectCharacter(ClickSelectCharacter character)
    {
        if (_selectCharacters[0] == null)
        {
            _selectCharacters[0] = character;
        }
        else if (_selectCharacters[1] == null)
        {
            _selectCharacters[1] = character;
            InitBattle();　//characterが2体セットされたらBattleを開始する
            _selectCharacters[0] = null;
            _selectCharacters[1] = null;
        }
    }
}