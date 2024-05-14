using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Uiの管理</summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _turnPanel;
    [SerializeField] private Text _text;
    public GameObject _button;

    //メインフェイズUI
    public void SetMainPanel()
    {
        _text.text = "MainPhase";
        _turnPanel.SetActive(true);
        StartCoroutine(SetTurnPanel());
    }

    //リフレッシュフェイズUI
    public void SetRefreshPanel()
    {
        _text.text = "RefreshPhase";
        _turnPanel.SetActive(true);
        StartCoroutine(SetTurnPanel());
    }

    //バトルフェイズUI
    public void SetBattlePanel()
    {
        _text.text = "BattlePhase";
        _turnPanel.SetActive(true);
        _button.SetActive(true);
        StartCoroutine(SetTurnPanel());
    }

    //ターンエンドフェイズUI
    public void SetTurnEndPanel()
    {
        _text.text = "TurnEnd";
        _turnPanel.SetActive(true);
        StartCoroutine(SetTurnPanel());
    }

    //Panelをsetする
    private IEnumerator SetTurnPanel()
    {
        yield return new WaitForSeconds(1f);
        _turnPanel.SetActive(false);
    }

    //EndPhaseに移行するためのButton
    public void OnclickEndPhase()
    {
        GameManager.Instance._turn = GameManager.Turn.EndPhase;
        _button.SetActive(false);
        GameManager.Instance._isPanelActive = true;
    }
}