using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HitDisplayer : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Camera _main;
    [SerializeField] private Character _enemy;
    [SerializeField] private GameObject _hitPrefab;
    [SerializeField] private float _lifeTime;

    [SerializeField] private Vector2 _xRange;
    [SerializeField] private Vector2 _yRange;

    private void Awake()
    {
        _enemy.OnHit += (damage) => DisplayHit();
    }

    private void DisplayHit()
    {
        GameObject hit = Instantiate(_hitPrefab, _parent.transform);

        var rtransform = hit.GetComponent<RectTransform>();

        rtransform.anchoredPosition = GetRandomPoint();
        rtransform.localScale = Vector3.zero;
        rtransform.DOScale(Vector3.one, _lifeTime / 4).OnComplete(() => rtransform.DOScale(Vector3.zero, _lifeTime/4*3));



        Destroy(hit, _lifeTime);
    }

    private Vector2 GetRandomPoint()
    {
        return new Vector2(Random.Range(_xRange.x, _xRange.y), Random.Range(_yRange.x, _yRange.y));
    }
}
