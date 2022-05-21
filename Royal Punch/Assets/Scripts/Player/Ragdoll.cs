using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using System;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private float _delayToStand = 3;
    [SerializeField] private float _standUpDuration = 0.5f;
    [SerializeField] private bool _isStangingAfterFalling = true;

    [SerializeField] private Collider[] _mainColliders;
    [SerializeField] private Rigidbody _hip;

    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody _rigidbodyToPunch;

    [SerializeField] private Transform _camera;

    private bool _isFallen;
    private Rigidbody[] _rigidbodies;
    private bool _isFoundRigids;
    private Vector3 _saveHipPos;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private bool _isFollowing;

    public bool IsFallen => _isFallen;
    public float StandUpDuration => _standUpDuration;

    public event Action OnFall;
    public event Action OnBeginStanding;
    public Action OnStandedUp;

    private void Awake()
    {
        OnBeginStanding += BeginStanding;
        OnStandedUp += ReturnValuesToStanded;
    }

    private void Start()
    {
        SetRigidbodyState(true);
        SetColliderState(false);

       //Invoke(nameof(Fall), 2f);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PunchRigidbody(5);
            Fall();
        }
        if (_isFollowing)
        {
            _endPos = _rigidbodyToPunch.transform.localPosition;
            //_camera.transform.position = Vector3.Lerp(_camera.transform.position, _camera.transform.position + _startPos - _endPos, Time.deltaTime);
        }
    }

    public void Fall()
    {
        if (!_isFallen)
        {
            //_playerAnimator.enabled = false;
            SetRigidbodyState(false);
            SetColliderState(true);

            if (_isStangingAfterFalling)
                Invoke(nameof(Stand), _delayToStand);

            OnFall?.Invoke();

            _isFallen = true;
        }
    }

    public void Stand()
    {
        OnBeginStanding?.Invoke();
    }

    public void PunchRigidbody(float force)
    {
        _isFollowing = true;
        _startPos = _rigidbodyToPunch.transform.position;
         _rigidbodyToPunch.AddForce(-transform.forward * force, ForceMode.Impulse);
        //_rigidbodyToPunch.AddExplosionForce(500, transform.position + transform.forward, 50);
    }

    private void SetRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies;
        if (!_isFoundRigids)
        {
            rigidbodies = GetComponentsInChildren<Rigidbody>();
            _rigidbodies = rigidbodies;
            _isFoundRigids = true;
        }
        else rigidbodies = _rigidbodies;
        SavePositions();
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

    private void SavePositions()
    {
        _saveHipPos = _hip.transform.position;
    }

    private void ReturnValuesToStanded()
    {
        print("Standart");
        SetRigidbodyState(true);
        SetColliderState(false);
       // _playerAnimator.enabled = true;
        _isFallen = false;
    }

    private void BeginStanding()
    {
        _isFollowing = false;
        //transform.position += new Vector3(0, 0, (_endPos - _startPos).z);
        //_rigidbodyToPunch.transform.localPosition = _startPos;
        _hip.DOMove(_saveHipPos, _standUpDuration).OnComplete(() => OnStandedUp?.Invoke());
       // Invoke(nameof(ReturnValuesToStanded), 0.5f);
    }
}
