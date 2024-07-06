using UnityEngine;
using UnityEngine.UI;

public class BattlePhase : PanelSetUp
{
    [SerializeField] private OnlineGameManager _onlineGameManager;
    [SerializeField] private GameObject _battlePanel;
    [SerializeField] private Text _phaseText;
    [SerializeField] private GameObject _button;
    private readonly ClickSelectCharacter[] _selectCharacters = new ClickSelectCharacter[2];
    [SerializeField] private RuleBook _ruleBook;
    
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
        Debug.Log("Battleの処理中");
        if(_selectCharacters[0] == null || _selectCharacters[1] == null) return;
        
        Debug.Log("2体のキャラクターを戦わせています");
        var power1 = _selectCharacters[0].PowerValue;
        var power2 = _selectCharacters[1].PowerValue;
        
        Debug.Log(power1);
        Debug.Log(power2);
        
        //自分のキャラクターが敵のキャラクターよりも上であれば
        if (power1 >= power2)
        {
            var random = Random.Range(0, 2);
            //boolがtrueの分だけTrapカードの処理を走らせる
            if (_onlineGameManager.IsFirstTrap)
            {
                Debug.Log("TrapCardOpen");
                _ruleBook.SetRule(0);
                random = random == 0 ? 1 : 0;
            }
        
            if (_onlineGameManager.IsSecondTrap)
            {
                Debug.Log("さらにTrapCardをOpen");
                _ruleBook.SetRule(random);
            }
            
            Debug.Log("Battle終了");
        }
        
        // キャラクター選択をリセット
        _selectCharacters[0] = null;
        _selectCharacters[1] = null;
    }
    
    /// <summary>選択されたcharacterをセットしてBattleを開始する</summary>
    public void SelectCharacter(ClickSelectCharacter character)
    {
        if (_selectCharacters[0] == null)
        {
            Debug.Log("1体目のキャラクターを保存");
            _selectCharacters[0] = character;
        }
        else if (_selectCharacters[1] == null)
        {
            Debug.Log("2体目のキャラクターを保存");
            _selectCharacters[1] = character;
            InitBattle();　//characterが2体セットされたらBattleを開始する
        }
    }
}
