using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FlyingHP : MonoBehaviour
{
    private Color _textColor;
    private TextMeshPro _text;

    public void Initialise(int value, float lifeTime, float flyingHeight)
    {
        _text = GetComponent<TextMeshPro>();
        _text.text = value.ToString();

        float alpha = 1;
        _textColor = _text.color;

        StartCoroutine(nameof(Disapear));
        //DOTween.To(() => 1, x => alpha = x, 0, lifeTime).OnUpdate(() => ApplyAlpha(alpha));

        transform.DOMove(transform.position + transform.up * flyingHeight, lifeTime/5*4);
        Destroy(gameObject, lifeTime);
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
