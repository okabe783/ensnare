using System.Collections;
using UnityEngine;

public class PanelSetUp : MonoBehaviour
{
    /// <summary>各Phaseの開始時にPanelを有効にする</summary>
    /// <param name="panel"></param>
    public void BeginPhase(GameObject panel)
    {
        panel.SetActive(true);
        StartCoroutine(PanelActive());
        panel.SetActive(false);
    }
    
    private static IEnumerator PanelActive()
    {
        yield return new WaitForSeconds(1f);
    }
}
