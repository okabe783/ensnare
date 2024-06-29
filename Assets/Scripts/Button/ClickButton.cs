using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

/// <summary>UI実装Manager</summary>
public class ClickButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private OnlineGameManager _onlineGameManager;
    [SerializeField] private Button _endTurnButton;

    private void Start()
    {
        // ボタンのクリックイベントにメソッドを登録
        _endTurnButton.onClick.AddListener(OnEndTurnButtonClicked);
    }
    
    /// <summary>ターン終了ボタンがクリックされたときに呼び出されるメソッド</summary>
    private void OnEndTurnButtonClicked()
    {
        EndToChange();
    }

    /// <summary>ターンを終了し、次のターンに進めるメソッド</summary>
    private void EndToChange()
    {
        _onlineGameManager.EndTurn();
        _endTurnButton.interactable = false;
    }
}
