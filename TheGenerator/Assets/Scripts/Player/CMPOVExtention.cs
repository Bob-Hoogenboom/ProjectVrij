using System;
using UnityEngine;
using Cinemachine;

public class CMPOVExtention : CinemachineExtension
{
    [Header("References")]
    private InputManager _inputManager;

    [Header("Extention Stats")]
    [Range(5f, 15f)] [SerializeField] private float horizontalSpeed = 10f;
    [Range(5f, 15f)] [SerializeField] private float verticalSpeed = 10f;
    [SerializeField] private float clampAngle = 80f;

    private Vector3 _startRotation;

    protected override void Awake()
    {
        _inputManager = InputManager.Instance;
        _startRotation = transform.localRotation.eulerAngles;
        base.Awake();
    }

    //This function overrides certain stats in the Virtual Camera so we can use the new Unity Input System on the cinemachine plugin
    //We also clampt the Y value to not loop around the player
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (_startRotation == null) _startRotation = transform.localRotation.eulerAngles;
                Vector2 deltaInput = _inputManager.GetMouseDelta();
                _startRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                _startRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;

                _startRotation.y = Mathf.Clamp( _startRotation.y, -clampAngle, clampAngle);

                //The delta Y(looking up/down) we want to locally rotate that on the X so for Euler angles we swap the X/Y _startRotation values
                state.RawOrientation = Quaternion.Euler(_startRotation.y, _startRotation.x, 0);
            }
        }
    }
}
