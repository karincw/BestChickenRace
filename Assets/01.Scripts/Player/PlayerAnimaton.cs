using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimaton : MonoBehaviour
{
    SpriteRenderer _spriterenderer;
    Animator _animator;

    private void Awake()
    {
        _spriterenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void SetAniSpeedValue(float value)
    {
        _animator.SetFloat("Speed", value);
    }
}
