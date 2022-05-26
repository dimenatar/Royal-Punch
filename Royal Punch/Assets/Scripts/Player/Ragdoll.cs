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
    [SerializeField] private Rigidbody _head;

    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody _rigidbodyToPunch;

    [SerializeField] private Transform _camera;
    [SerializeField] private RagdollSaver _ragdollSaver;

    private bool _isFallen;
    private bool _isInitialised;
    private Rigidbody[] _rigidbodies;
    private bool _isFoundRigids;
    private Vector3 _saveHipPos;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private bool _isFollowing;
    private float _timer = 0;

    private bool _isStandingSave;

    private List<Vector3> _temp;
    private List<Quaternion> _temp2;

    public bool IsFallen => _isFallen;
    public float StandUpDuration => _standUpDuration;

    public event Action OnFall;
    public event Action OnBeginStanding;
    public event Action OnStandedUp;

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

    public void Initialise()
    {
        if (_isInitialised)
        {
            _isStangingAfterFalling = _isStandingSave;
        }
        else
        {
            _isStandingSave = _isStangingAfterFalling;
            _isInitialised = true;
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

    private void Stand()
    {
        if (_isStangingAfterFalling)
        OnBeginStanding?.Invoke();
    }

    public void PunchRigidbody(float force)
    {
        _isFollowing = true;
        _startPos = _rigidbodyToPunch.transform.position;
         _rigidbodyToPunch.AddForce(-transform.forward * force, ForceMode.Impulse);
        //_rigidbodyToPunch.AddExplosionForce(500, transform.position + transform.forward, 50);
    }

    public void Restore()
    {
        SetRigidbodyState(true);
        SetColliderState(false);
        _isFallen = false;
        OnStandedUp?.Invoke();
    }

    public void FullyFall()
    {
        _isStangingAfterFalling = false;
        Fall();
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
        SavePositions(_rigidbodies);
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
        _saveHipPos = _head.transform.localPosition;
        _ragdollSaver.Rewrite();
        foreach (var item in rigidbodies)
        {
            _ragdollSaver.WriteValue(new Bone(item.name, item.transform.position, item.transform.rotation));
        }
    }

    private void ReturnValuesToStanded()
    {
        SetRigidbodyState(true);
        SetColliderState(false);
       // _playerAnimator.enabled = true;
        _isFallen = false;
    }

    private void BeginStanding()
    {
        Vector3 headPos = _ragdollSaver.Bones.Where(b => b.Name == _head.name).FirstOrDefault().Position;
        StartCoroutine(nameof(LerpHead));
    }

    private void RestoreBones()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        StartCoroutine(nameof(Lerp));
    }

    private IEnumerator LerpHead()
    {
        _timer = 0;
        Bone head = _ragdollSaver.Bones.Where(b => b.Name == _head.name).FirstOrDefault();
        Vector3 startPoint = _head.transform.position;
        while (_timer < _standUpDuration)
        {
            _head.transform.position = Vector3.Lerp(startPoint, new Vector3(_head.transform.position.x, head.Position.y, _head.transform.position.z), _timer / _standUpDuration);
            _timer += Time.deltaTime;
            yield return null;
        }
        RestoreBones();
    }

    private IEnumerator Lerp()
    {
        _temp = new List<Vector3>();
        _temp2 = new List<Quaternion>();
        //store currentPos

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _temp.Add(_rigidbodies[i].transform.position);
            _temp2.Add(_rigidbodies[i].transform.rotation);
        }

        _timer = 0;
        while (_timer < _standUpDuration)
        {
            for (int i = 0; i < _rigidbodies.Length; i ++)
            {
                //_rigidbodies[i].transform.SetPositionAndRotation(Vector3.Lerp(_temp[i], _ragdollSaver.Bones.Where(b => b.Name == _rigidbodies[i].name).FirstOrDefault().Position, _timer), Quaternion.Euler(Vector3.Lerp(_temp2[i].eulerAngles, _ragdollSaver.Bones.Where(b => b.Name == _rigidbodies[i].name).FirstOrDefault().Rotation.eulerAngles, _timer/_delayToStand)));
                //_rigidbodies[i].transform.position = Vector3.Lerp(_temp[i], _ragdollSaver.Bones.Where(b => b.Name == _rigidbodies[i].name).FirstOrDefault().Position, _timer / _standUpDuration);
                _rigidbodies[i].transform.SetPositionAndRotation(Vector3.Lerp(_temp[i], _ragdollSaver.Bones[i].Position, _timer/ _standUpDuration), 
                    Quaternion.Euler(Vector3.Lerp(_temp2[i].eulerAngles, _ragdollSaver.Bones[i].Rotation.eulerAngles, _timer/ _standUpDuration)));
                _timer += Time.deltaTime;
                yield return null;
            }
        }
        OnStandedUp?.Invoke();
    }
}
