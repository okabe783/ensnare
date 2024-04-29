using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable /Create Card")]
public class CardDataBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _cardId;
    [SerializeField] private string _cardType;
    [SerializeField] private Sprite iconImage;
    [TextArea] private string _description;

    public Sprite Icon => iconImage;
    public string Description => _description;
}