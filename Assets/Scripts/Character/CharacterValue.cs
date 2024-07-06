using UnityEngine;

public class CharacterValue : MonoBehaviour
{
    public int PowerValue { get; set;}

    /// <summary>Powerを加算する</summary>
    public void GetPower(int value)
    {
        PowerValue += value;
    }

    /// <summary>Powerを減算する</summary>
    public void DownValue(int value)
    {
        PowerValue -= value;
    }

    /// <summary>Powerをリセットする</summary>
    public void ResetValue()
    {
        PowerValue = 0;
    }
}
