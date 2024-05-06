using UnityEngine;

/// <summary>
/// Cardを生成する為のscript
/// GeneratorObjectにアタッチする
/// </summary>
public class CardGenerator : MonoBehaviour
{
    public CardDataBase[] _playerCardDataBases;
    public Card _card;

    /// <summary>Cardを生成</summary>
    public Card CardSpawn(int cardNumber)
    {
        var card = Instantiate(_card);
        //CardがPlayer用なら
        card.CardSet(_playerCardDataBases[cardNumber]);

        return card;
    }
}