using UnityEngine;

public class RuleBook : MonoBehaviour
{
    [SerializeField] private OnlineGameManager _onlineGameManager;
    [SerializeField] private TrapRuleBook _trapRule;
    [SerializeField] private DeathCardRule _lose;

    /// <summary>
    /// Battle終了時負けたcharacterのカードをOpenする
    /// </summary>
    /// <param name="rule"></param>
    public void SetRule(int rule)
    {
        //TrapCardであれば
        if (_onlineGameManager.IsFirstTrap && rule == 0)
        {
            Debug.Log("Bindを発動");
            _trapRule.Bind();
        }
        else if (_onlineGameManager.IsFirstTrap && rule == 1)
        {
            _trapRule.DownValue();
            Debug.Log("攻撃力低下");
        }
    }

    public void Lose()
    {
        //敗北カードであれば
        _lose.GameLose();
    }
}