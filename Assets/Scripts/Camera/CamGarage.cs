using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamGarage : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float sensitivity = 3;
    public float limit = 80;
    public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
    public float zoomMax = 10; // макс. увеличение
    public float zoomMin = 3; // мин. увеличение
    private float X, Y;
    private bool _canRotate = false;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    void Start()
    {
        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90;

        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            _canRotate = true;
        }

        if(Input.GetMouseButton(0))
        {
           _canRotate = false;
           transform.position = _startPosition;
           transform.rotation = _startRotation;
        }

        if (_canRotate == true)
        {
            X += Input.GetAxis("Mouse X") * sensitivity;
            Y -= Input.GetAxis("Mouse Y") * sensitivity;
            Y = Mathf.Clamp(Y, -limit, limit);
            transform.localEulerAngles = new Vector3(-Y, X, 0);
            transform.position = transform.localRotation * offset + target.position;
        }
    }
}