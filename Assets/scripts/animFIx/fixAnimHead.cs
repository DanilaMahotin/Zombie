using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixAnimHead : MonoBehaviour
{
    float _moveInput;
    float mobileMove;
    Animator _anim;
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        _moveInput = playerMove.moveInput;
        mobileMove = playerMove.mobileMove;
        

        _anim.SetBool("headFix", _moveInput > 0 || _moveInput < 0 || mobileMove < 0 || mobileMove > 0);

    }
}
