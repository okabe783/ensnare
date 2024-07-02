using UnityEngine;

/// <summary>Cardを生成する</summary>
public class CardGenerator : MonoBehaviour
{
    [SerializeField, Header("Masterの手札を管理するPosition")] private HandPosition _masterHandPos;

    [SerializeField, Header("Guestの手札を管理するPosition")] private HandPosition _guestHandPos;
    
   [SerializeField] private CardDataBase[] _cardDataBases;  //ScriptableObjectを配列に追加
    
    [SerializeField,Header("カードオブジェクト")] private GameObject _cardPrefab;
    [SerializeField, Header("敵のカード")]　private GameObject _enemyCardPrefab;

    private CardSelector _cardSelector;　//カードの状態をCheck

    private void Start()
    {
        _cardSelector = FindObjectOfType<CardSelector>();
        
        if (_cardSelector == null) return;
        _cardSelector.MasterHandPosition = _masterHandPos;
        _cardSelector.GuestHandPosition = _guestHandPos;
    }

    /// <summary>Master用のCardを生成</summary>
    public void MasterCardSpawn(int cardNumber, bool isPlayer)
    {
        //MasterかGuestか
        var spawnPosition = isPlayer ? _masterHandPos.transform.position : _guestHandPos.transform.position;

        if (isPlayer)
        {
            //Masterのカードを生成
            var createCard = Instantiate(_cardPrefab, spawnPosition, Quaternion.identity);
            var card = createCard.GetComponent<Card>();
            
            card.CardSet(_cardDataBases[cardNumber]);　//Cardの情報を読み込む
            card.IsPlayer = true; //Cardに登録
            AddCardToHand(card, true);　
        }
        else
        {
            //Guestのカードを生成
            var createCard = Instantiate(_enemyCardPrefab, spawnPosition, Quaternion.identity);
            var card = createCard.GetComponent<Card>();
            AddCardToHand(card,false);
        }
    }
    
    /// <summary>Guest用のCardを生成</summary>
    public void GuestCardSpawn(int cardNumber, bool isPlayer)
    {
        var spawnPosition = isPlayer ? _masterHandPos.transform.position : _guestHandPos.transform.position;
        
        if (!isPlayer)
        {
            
            var createCard = Instantiate(_cardPrefab, spawnPosition, Quaternion.identity);
            var card = createCard.GetComponent<Card>();
            
            card.CardSet(_cardDataBases[cardNumber]);
            card.IsPlayer = false;
            AddCardToHand(card, false);
        }
        else
        {
            var createCard = Instantiate(_enemyCardPrefab, spawnPosition, Quaternion.identity);
            var card = createCard.GetComponent<Card>();
            AddCardToHand(card,true);
        }
    }

    /// <summary>手札にCardを追加</summary>
    private void AddCardToHand(Card card, bool isPlayer)
    {
        var handPos = isPlayer ? _masterHandPos : _guestHandPos;
        handPos.Add(card, isPlayer);
        card.OnClickCard = OnClickCard;　//イベントアクションに登録する
    }

    //通知を送る
    private void OnClickCard(Card card)
    {
        _cardSelector.NotifyCardSelected(card);
        _cardSelector.SetChoiceCard(card, card.IsPlayer);
    }

    /// <summary>手札の位置を調整する</summary>
    public void SortHand()
    {
        _masterHandPos.ResetHandPosition(true);
        _guestHandPos.ResetHandPosition(false);
    }
}