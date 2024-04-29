using UnityEngine;
using static CardGenerator;

public class GameManager : MonoBehaviour
{
    public GameObject _mainPhase;

    [SerializeField] private CardGenerator _cardGenerator;

    private void Awake()
    {
        _mainPhase.SetActive(false);
    }

    private void Start()
    {
        _cardGenerator.CardSpawn(CardType.Player);
    }
}
