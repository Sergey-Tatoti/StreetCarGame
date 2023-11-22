using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 3;
    [SerializeField] private float _limitY = 40;
    [SerializeField] private float _positionSpeedReturn = 4;
    [SerializeField] private float _rotationSpeedReturn = 2;

    private Vector3 _offset;
    private Transform _target;
    private float _xPosition;
    private float _yPosition;

    public void SetValues(Transform target)
    {
        _target = target;
        _offset = transform.position - _target.position;
        _limitY = Mathf.Abs(_limitY);

        if (_limitY > 90)
            _limitY = 90;
    }

    public void Move()
    {
        _xPosition += Input.GetAxis("Mouse X") * _sensitivity;
        _yPosition -= Input.GetAxis("Mouse Y") * _sensitivity;
        _yPosition = Mathf.Clamp(_yPosition, -_limitY, _limitY);

        transform.localEulerAngles = new Vector3(-_yPosition, _xPosition, 0);
        transform.position = transform.localRotation * _offset + _target.position;
    }

    public IEnumerator MoveReturn(Vector3 newPosition, Vector3 newRotation)
    {
        float elipsedTime = 0;

        _xPosition = 0;
        _yPosition = 0;

        while(transform.position != newPosition)
        {
            elipsedTime += Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, newPosition, elipsedTime * _positionSpeedReturn);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRotation), elipsedTime * _rotationSpeedReturn);

            yield return null;
        }
    }
}
