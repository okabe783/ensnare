using System.Collections;
using UnityEngine;

public class PanelSetUp : MonoBehaviour
{
    /// <summary>各Phaseの開始時にPanelを有効にする</summary>
    protected void BeginPhase(GameObject panel)
    {
        panel.SetActive(true);
        StartCoroutine(PanelActive(panel));
    }
    
    private static IEnumerator PanelActive(GameObject panel)
    {
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }
}
