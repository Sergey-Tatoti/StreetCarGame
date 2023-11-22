using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGarage : MonoBehaviour
{
    [SerializeField] private CameraTouchTracker _cameraTouchTracker;
    [SerializeField] private CameraMoving _cameraMoving;
    [SerializeField] private Transform _target;

    private Coroutine _moving;
    private Vector3 _startPosition;
    private Vector3 _startRotation;
    private bool _canRotate = false;
    private bool _isLocked = false;
    private bool _isMove = false;

    private void OnEnable()
    {
        _cameraTouchTracker.PressedClick += OnPressedClick;
    }

    private void OnDisable()
    {
        _cameraTouchTracker.PressedClick -= OnPressedClick;
    }

    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.eulerAngles;

        _cameraMoving.SetValues(_target);
    }

    void Update()
    {
        if (_isLocked == false)
        {
            if (_canRotate == true)
            {
                if (_moving != null)
                    StopCoroutine(_moving);

                _cameraMoving.Move();
                _isMove = false;
            }

            if (_canRotate == false && _isMove == false)
            {
                _isMove = true;
                ReturnStartPosition();
            }
        }
    }

    public void LockCamera(bool isLock)
    {
        _isLocked = isLock;
    }

    public void ChangePositionCamera(Vector3 newPosition, Vector3 newRotation)
    {
        StartCoroutine(_cameraMoving.MoveReturn(newPosition, newRotation));
    }

    public void ReturnStartPosition()
    {
        StartCoroutine(_cameraMoving.MoveReturn(_startPosition, _startRotation));
    }

    private void OnPressedClick(bool isPressed)
    {
        if (isPressed == true)
            _canRotate = true;
        else
            _canRotate = false;
    }
}