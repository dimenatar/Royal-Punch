using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void onTime();
    public event onTime OnTime;

    private float _delay;
    private float _time;
    private bool _isStarted;
    private bool _isRepeating;

    public bool IsStarted => _isStarted;

    public void Initialise(float delay, bool startOnInit = false, bool repeating = false) 
    {
        _time = 0;
        _delay = delay;
        _isRepeating = repeating;
        if (startOnInit)
        {
            _isStarted = true;
        }
    }

    public void StartTimer()
    {
        _time = 0;
        _isStarted = true;
    }

    private void Update()
    {
        if (_isStarted)
        {
            if (_time >= _delay)
            {
                OnTime?.Invoke();
                if (!_isRepeating)
                _time = 0;
            }
            else
            {
                _time += Time.deltaTime;
            }
        }
    }

    public void ClearEvent()
    {
        OnTime = null;
    }

    public void StopTimer()
    {
        _isStarted = false;
        _time = 0;
    }

    public float GetPassedTime()
    {
        return _time;
    }

    public void UpdateTimer()
    {
        _time = 0;
        _isStarted = true;
    }

    public void UpdateTimer(float newDelay)
    {
        _delay = newDelay;
        _time = 0;
    }
}
