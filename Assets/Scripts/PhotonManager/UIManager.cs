using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("Cardを配る")] private CardGenerator _generator;
    
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
