using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTextSpawner : MonoBehaviour
{
    [SerializeField] private Character _character;

    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _hpLifetime = 1.5f;
    [SerializeField] private float _flyHeight = 10000;
    [SerializeField] private float _scalingTime = 0.5f;
    [SerializeField] private float _dissapearingTimeDelay = 2;

    [SerializeField] private Vector2 _minMaxRandomPosX;

    private void Awake()
    {
        _character.OnHit += SpawnHP;
    }

    public void SpawnHP(int value)
    {
        GameObject flyingText = Instantiate(_prefab, _parent);
        flyingText.transform.position = new Vector3(Random.Range(_minMaxRandomPosX.x, _minMaxRandomPosX.y), flyingText.transform.position.y, flyingText.transform.position.z);
        FlyingHP hp = flyingText.GetComponent<FlyingHP>();
        hp.Initialise(value, _hpLifetime, _flyHeight, _scalingTime, _dissapearingTimeDelay, Random.Range(_minMaxRandomPosX.x, _minMaxRandomPosX.y));
        //hp.Initialise(value, new Vector3(0,0,0), _hpLifetime, _flyHeight);
        flyingText.SetActive(true);
    }
}
