using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    [SerializeField] private Collider[] _mainColliders;
    [SerializeField] private List<Rigidbody> _legs;

    [SerializeField] private float _delayToStand;
    [SerializeField] private bool _isStangingAfterFalling = true;

    private bool _isFallen;

    public bool IsFallen => _isFallen;

    private List<Vector3> _storedPositions;
    private List<Quaternion> _storedRotations;
    private Rigidbody[] _rigidbodies;
    private bool _isFoundRigids;


    private void Start()
    {
        SetRigidbodyState(true);
        SetColliderState(false);

        Fall();
    }

    public void Fall()
    {
        _playerAnimator.enabled = false;
        SetRigidbodyState(false);
        SetColliderState(true);

        if (_isStangingAfterFalling)
        Invoke(nameof(Stand), _delayToStand);
        _isFallen = true;
    }

    public void Stand()
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].transform.DOMove(_storedPositions[i], 0.5f);
            //_rigidbodies[i].transform.DORotate(_storedRotations[i].ToEulerAngles(), 0.5f);
        }

        for (int i = 0; i < _legs.Count; i++)
        {
            _legs[i].DORotate(_storedRotations[i].ToEulerAngles(), 0.5f);
        }
        Invoke(nameof(ReturnValuesToStanded), 0.5f);

    }

    private void SetRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies;
        if (!_isFoundRigids)
        {
            rigidbodies = GetComponentsInChildren<Rigidbody>();
            _rigidbodies = rigidbodies;
        }
        else rigidbodies = _rigidbodies;
        SavePositions(rigidbodies);
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        GetComponent<Rigidbody>().isKinematic = !state;
    }

    private void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        foreach (Collider collider in _mainColliders)
        {
            collider.enabled = !state;
        }
    }

    private void SavePositions(Rigidbody[] rigidbodies)
    {
        _storedPositions = new List<Vector3>();
        _storedRotations = new List<Quaternion>();
        rigidbodies.ToList().ForEach(r => _storedPositions.Add(r.transform.position));
        // rigidbodies.ToList().ForEach(r => _storedRotations.Add(r.transform.rotation));
        foreach (var item in _legs)
        {
            var r = rigidbodies.ToList().Where(r => r == item).FirstOrDefault();
            _storedRotations.Add(r.transform.rotation);
        }

       //
    }

    private void ReturnValuesToStanded()
    {
        SetRigidbodyState(true);
        SetColliderState(false);
        _playerAnimator.enabled = true;
        _isFallen = false;
    }
}
