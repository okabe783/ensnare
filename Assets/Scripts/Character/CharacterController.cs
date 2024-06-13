using Ensnare.Enums;
using UnityEngine;

/// <summary>Animationの制御</summary>
public class CharacterController : MonoBehaviour
{
    private Animator _animator;

    private int _hashStartPhase = Animator.StringToHash("StartPhase");
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PhaseAnimation();
    }

    //StartPhase開始時にアニメーションを実行
    private void PhaseAnimation()
    {
        if (GameManager.Instance._turn == Phase.StartPhase)
        {
            _animator.SetTrigger(_hashStartPhase);
        }
    }
}
