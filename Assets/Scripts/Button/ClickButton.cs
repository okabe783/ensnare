using UnityEngine;
using Ensnare.Enums;
using Photon.Pun;

/// <summary>UI実装Manager</summary>
public class ClickButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private TurnChange _turnChange;
    [SerializeField] private GameObject _buttonUI;
    private IButton _button;

    private void Awake()
    {
        _button = _turnChange;
    }

    /// <summary>指定の関数を全てのクライアントに送る</summary>
    public void OnClickTurnChange()
    {
        photonView.RPC("RPCOnclickTurnChange",RpcTarget.All);
    }

    /// <summary>Turnを切り替える</summary>
    [PunRPC]
    public void RPCOnclickTurnChange()
    {
        if (_turnChange.CurrentTurn == Turn.GuestTurn)
            _button.OnClickMasterTurn();
        else
            _button.OnClickGuestTurn();
    }
}
