using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable /Create Card")]
public class CardDataBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _cardId;
    [SerializeField] private string _cardType;
    [SerializeField] private Image iconImage;
    [TextArea] private Text _description;

    public Image Icon => iconImage;
    public Text Description => _description;
}