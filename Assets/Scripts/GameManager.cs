using UnityEngine;
using static CardGenerator;

public class GameManager : MonoBehaviour
{
    public GameObject _mainPhase;

    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private HandPosition _handPosition;

    private void Awake()
    {
        _mainPhase.SetActive(false);
    }

    private void Start()
    {
        DrawFirstHand();
    }

    private void DrawFirstHand()
    {
        for (var i = 0; i < 6; i++)
        {
            var card = _cardGenerator.CardSpawn(CardType.Player); //Cardを配る
            _handPosition.Add(card); //playerの手札に追加
        }
        _handPosition.ResetPosition();
    } 
}
