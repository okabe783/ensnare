using UnityEngine;
using UnityEngine.UI;

/// <summary>CardPrefabにアタッチする</summary>
public class Card : MonoBehaviour
{
    public CardDataBase _cardDataBase { get; private set; }

    private Vector3 _offSet;
    private float _zCoord;

    //CardUI
    [SerializeField] private Image _icon;
    [SerializeField] private Text _description;

    //ScriptableObjectで設定したCardを読み込む
    public void CardSet(CardDataBase cardDataBase)
    {
        _cardDataBase = cardDataBase;
        _icon = _cardDataBase.Icon;
        _description = _cardDataBase.Description;
    }

    /// <summary>MouseがClickされたときに呼び出される</summary>
    private void OnMouseDown()
    {
        _zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        _offSet = gameObject.transform.position - GetMouseWorldPos();
    }

    /// <summary>Mouseのscreen座標をworld座標に変換する</summary>
    private Vector3 GetMouseWorldPos()
    {
        var mousePoint = Input.mousePosition;
        mousePoint.z = _zCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    /// <summary>
    /// Mouseのdrag中に呼び出される
    /// </summary>
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + _offSet; //cardの位置を更新
    }
}