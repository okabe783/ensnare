using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _mainPhase;

    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private HandPosition _hand;
    [SerializeField] private SelectedCard _selectedCard;
    [SerializeField] private Player _player;

    private void Awake()
    {
        _mainPhase.SetActive(false);
    }

    private void Start()
    {
        DrawFirstHand(_player);
    }

    private void DrawFirstHand(Player player)
    {
        for (var i = 0; i < 6; i++)
        {
            var card = _cardGenerator.CardSpawn(i); //Cardを配る
            player.SetCardToHand(card);
        }
        player.Hand.ResetPosition();
    }
}