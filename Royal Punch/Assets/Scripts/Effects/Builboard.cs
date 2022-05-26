using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builboard : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        _transform.LookAt(_transform.position + _camera.forward);
    }
}
