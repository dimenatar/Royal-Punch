using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FlyingHP : MonoBehaviour
{
    private Color _textColor;
    private TextMeshPro _text;

    public void Initialise(int value, float lifeTime, float flyingHeight, float scalingTime, float startDissapearingDelay)
    {
        _text = GetComponent<TextMeshPro>();
        _text.text = value.ToString();

        _textColor = _text.color;

        transform.DOScale(0.4f, scalingTime);
        //DOTween.To(() => 1, x => alpha = x, 0, lifeTime).OnUpdate(() => ApplyAlpha(alpha));
        Invoke(nameof(StartDissapearing), startDissapearingDelay);
        transform.DOMove(transform.position + Vector3.up * flyingHeight, lifeTime/5*4);
        Destroy(gameObject, lifeTime);
    }

    private void StartDissapearing()
    {
        StartCoroutine(nameof(Disapear));
    }

    private IEnumerator Disapear()
    {
        while (_textColor.a > 0)
        {
            _textColor.a -= Time.deltaTime;
            _text.color = _textColor;
            yield return null;
        }
    }
}
