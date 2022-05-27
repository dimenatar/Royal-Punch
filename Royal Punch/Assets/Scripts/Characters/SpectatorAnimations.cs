using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorAnimations : MonoBehaviour
{
    [SerializeField] private int _animAmount;
    [SerializeField] private Animator _animator;

    private readonly int _animIndex = Animator.StringToHash("index");

    private void Start()
    {
        PickRandomAnim();
    }

    private void PickRandomAnim()
    {
        _animator.SetInteger(_animIndex, Random.Range(0, _animAmount));
    }
}
