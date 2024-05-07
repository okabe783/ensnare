using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private Player _player;

    private void Start()
    {
        DrawFirstHand(_player);
    }

    /// <summary>GameがStartしたときhandを配る</summary>
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