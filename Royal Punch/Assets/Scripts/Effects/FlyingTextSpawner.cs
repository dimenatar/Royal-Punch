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

    private void Awake()
    {
        _character.OnHit += SpawnHP;
    }

    public void SpawnHP(int value)
    {
        GameObject flyingText = Instantiate(_prefab, _parent);

        FlyingHP hp = flyingText.GetComponent<FlyingHP>();
        hp.Initialise(value, _hpLifetime, _flyHeight);
        //hp.Initialise(value, new Vector3(0,0,0), _hpLifetime, _flyHeight);
        flyingText.SetActive(true);
    }
}
