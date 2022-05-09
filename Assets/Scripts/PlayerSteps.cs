using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerSteps : AbstractProgress
{
    private Animator _animator;
    private bool _isRightLegAvailable = true;
    private bool _isLeftLegAvailable = true;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void RightLeg()
    {
        if (_isRightLegAvailable && CameraSwitcher.GameActivated)
        {
            ProgressUp();
            _animator.Play("Red_EnFaceLeftLeg");
            _isRightLegAvailable = false;
            _isLeftLegAvailable = true;
        }
    }
    public void LeftLeg()
    {
        if (_isLeftLegAvailable && CameraSwitcher.GameActivated)
        {
            ProgressUp();
            _animator.Play("Red_EnFaceRightLeg"); 
             _isRightLegAvailable = true;
            _isLeftLegAvailable = false;
        }
    }

}
