using UnityEngine;

public class CreateCard : MonoBehaviour
{
    [SerializeField] private CardGenerator _generator;

    /// <summary>GameがStartしたときhandを配る</summary>
    public void DrawCard()
    {
        for (var i = 0; i < 6; i++)
        {
            _generator.CardSpawn(i); //Cardを配る
        }

        _generator.ResetPosition();
    }
}