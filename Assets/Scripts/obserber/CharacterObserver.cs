using UnityEngine;
using UniRx;

/// <summary>Characterの行動を監視する</summary>
public class CharacterObserver : MonoBehaviour
{
    private BoolReactiveProperty _isBind = new(false);
    public IReadOnlyReactiveProperty<bool>IsBind => _isBind;
    private System.Action<bool> _onBindStateChanged; //stateの変更を通知する

    //初期化
    public void CharacterInitialize(System.Action<bool> onBindStateChanged)
    {
        _onBindStateChanged = onBindStateChanged;

        IsBind.Subscribe(_isBind =>
        {
            _onBindStateChanged?.Invoke(_isBind);
        }).AddTo(this);

    }
    public void SetIsBind(bool value)
    {
        Debug.Log(_isBind);
        _isBind.Value = value;
    }
}
