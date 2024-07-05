using UnityEngine;

public class TrapRuleBook : MonoBehaviour
{
    [SerializeField] private OnlineGameManager _onlineGameManager;

    //TrapCardの効果
    public void Bind()
    {
        //このカードが表側表示にされた場合相手は次のターン後衛のキャラクターに
        //Cardを振り分けることができない
        _onlineGameManager.SetIsBind(true);
    }

    public void DownValue()
    {
        //このカードが表側表示にされた場合相手は次のターンCardの攻撃力を-100
        _onlineGameManager.DownPowerValue -= 100;
    }
}