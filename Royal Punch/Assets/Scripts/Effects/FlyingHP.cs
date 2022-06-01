using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FlyingHP : MonoBehaviour
{
    private Color _textColor;
    private TextMeshPro _text;

    public void Initialise(int value, float lifeTime, float flyingHeight, float scalingTime, float startDissapearingDelay, float horizontalPoint)
    {
        _text = GetComponent<TextMeshPro>();
        _text.text = value.ToString();

        _textColor = _text.color;

        transform.DOScale(0.4f, scalingTime);

        Invoke(nameof(StartDissapearing), startDissapearingDelay);

        var endPoint = Vector3.up * flyingHeight + 3 * horizontalPoint * Vector3.right;

        transform.DOMove(transform.position + endPoint/2, lifeTime/4).OnComplete(() => transform.DOMove(transform.position + endPoint / 2, lifeTime / 4*3));
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
