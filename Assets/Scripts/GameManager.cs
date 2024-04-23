using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _mainPhase;

    public void Start()
    {
        _mainPhase.SetActive(false);
    }
}
