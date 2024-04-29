using UnityEngine;

/// <summary>
/// Cardを生成する為のscript
/// GeneratorObjectにアタッチする
/// </summary>
public class CardGenerator : MonoBehaviour
{
    public CardDataBase[] _playerCardDataBases;
    public CardDataBase[] _enemyCardDataBases;
    public Card _card;

    /// <summary>Cardを生成</summary>
    public Card CardSpawn(CardType cardType)
    {
        var randomIndex = 0;
        var card = Instantiate(_card);
        //CardがPlayer用なら
        if (cardType == CardType.Player)
        {
            randomIndex = Random.Range(0, _playerCardDataBases.Length);
            card.CardSet(_playerCardDataBases[randomIndex]);
        }
        else
        {
            randomIndex = Random.Range(0, _enemyCardDataBases.Length);
            card.CardSet(_enemyCardDataBases[randomIndex]);
        }

        return card;
    }

    public enum CardType
    {
        Player,
        Enemy,
    }
}
