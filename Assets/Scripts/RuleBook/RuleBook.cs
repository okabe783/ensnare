using Ensnare.Enums;
using UnityEngine;

public class RuleBook : MonoBehaviour
{
    [SerializeField] private TrapRuleBook _trapRule;
    [SerializeField] private DeathCardRule _lose;

    /// <summary>
    /// Battle終了時負けたcharacterのカードをOpenする
    /// </summary>
    /// <param name="cardType"></param>
    /// <param name="rule"></param>
    public void SetRule(CardType cardType, int rule)
    {
        //TrapCardであれば
        switch (cardType)
        {
            case CardType.TrapCard when rule == 0:
                _trapRule.Bind();
                break;
            case CardType.TrapCard when rule == 1:
                _trapRule.DownValue();
                break;
        }

        //敗北カードであれば
        if (cardType == CardType.DeathCard)
            _lose.GameLose();
    }
}