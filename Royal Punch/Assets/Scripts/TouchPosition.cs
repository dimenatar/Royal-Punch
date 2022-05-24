using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPosition : MonoBehaviour
{
    [SerializeField] private GameObject _touch;
    [SerializeField] private SimpleTouchController _controller;
    [SerializeField] private Camera _main;

    private bool _isShown;
    private bool _isHolding;

    private void Update()
    {
        if (_isShown)
        {
           // if (!_isHodling && Input.touchCount > 0)
            if (!_isHolding && Input.GetMouseButtonDown(0))
            {
                // _touch.transform.position = _main.ScreenToWorldPoint(Input.GetTouch(0).position);
                _touch.transform.position = Input.mousePosition;
                _touch.SetActive(true);
                _controller.BeginDrag();
                _isHolding = true;
            }
        }
       // if (Input.touchCount > 0 && _isHodling)
       if (_isHolding)
        {
            //Touch touch = Input.GetTouch(0);
            //if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            if (Input.GetMouseButtonUp(0))
            {
                _isHolding = false;
                _controller.EndDrag();
                _touch.SetActive(false);
            }
        }
    }

    public void DisalbeTouch() => _isShown = false;
    public void Enabletouch() => _isShown = true;
}
