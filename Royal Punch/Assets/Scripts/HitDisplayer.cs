using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HitDisplayer : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _main;
    [SerializeField] private Character _enemy;
    [SerializeField] private GameObject _hitPrefab;
    [SerializeField] private float _lifeTime;

    private Vector2 _xRange;
    private Vector2 _yRange;

    private void Awake()
    {
        _enemy.OnHit += (damage) => DisplayHit();
    }

    private void Start()
    {
        CalculateBounds();
    }

    private void DisplayHit()
    {
        GameObject hit = Instantiate(_hitPrefab, _canvas.transform);

        var rtransform = hit.GetComponent<RectTransform>();

        rtransform.anchoredPosition = GetRandomPoint();
        rtransform.localScale = Vector3.zero;
        rtransform.DOScale(Vector3.one, _lifeTime / 4).OnComplete(() => rtransform.DOScale(Vector3.zero, _lifeTime/4*3));



        Destroy(hit, _lifeTime);
    }

    private void CalculateBounds()
    {
        //center pivot
        var reference = _canvas.GetComponent<CanvasScaler>().referenceResolution / 2;

        _xRange.x = -reference.x + _hitPrefab.GetComponent<RectTransform>().sizeDelta.x;
        _xRange.y = reference.x - _hitPrefab.GetComponent<RectTransform>().sizeDelta.x;

        _yRange.x = -reference.y + _hitPrefab.GetComponent<RectTransform>().sizeDelta.y;
        _yRange.y = reference.x - _hitPrefab.GetComponent<RectTransform>().sizeDelta.y;

        print(_xRange + " " + _yRange);
    }

    private Vector2 GetRandomPoint()
    {
        return new Vector2(Random.Range(_xRange.x, _xRange.y), Random.Range(_yRange.x, _yRange.y));
    }
}
