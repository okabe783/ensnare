using System;
using UnityEngine;
using Ensnare.Enums;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TurnChange _turnChange;

    private IButton _button;

    private void Awake()
    {
        _button = _turnChange;
    }

    private void OnClickTurnChange()
    {
        if (_turnChange.CurrentTurn == Turn.MasterTurn)
            _button.OnClickGuestTurn();
        else
            _button.OnClickMasterTurn();
    }
}
