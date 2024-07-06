using UnityEngine;

public class TrapRuleBook : MonoBehaviour
{
    [SerializeField] private OnlineGameManager _onlineGameManager;
    [SerializeField] private CharacterObserver _characterObserver;

    //TrapCardの効果
    public void Bind()
    {
        //このカードが表側表示にされた場合相手は次のターン後衛のキャラクターに
        //Cardを振り分けることができない
        Debug.Log("bool値をtrueにした");
        _characterObserver.SetIsBind(true);
    }

    public void DownValue()
    {
        //このカードが表側表示にされた場合相手は次のターンCardの攻撃力を-100
        _onlineGameManager.DownPowerValue -= 100;
    }
}