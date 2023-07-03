using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public Action<float> OnHasBeenUpdated;
    public Action OnTimeIsOver;
    private IEnumerator _countdown;
    private MonoBehaviour _context;
    private float _time;
    private float _remainingTime;

    public Timer(MonoBehaviour context)
    {
        _context = context;
    } 

    public void Set(float time)
    {
        _time = time;
        _remainingTime = _time;
    }

    public void StartCountingTime()
    {
        _countdown = Countdown();
        _context.StartCoroutine(_countdown);
    }

    public void StopCountingTime()
    {
        if (_countdown != null)
        {
            _context.StopCoroutine(_countdown);
        }
    }

    private IEnumerator Countdown()
    {
        while (_remainingTime >= 0)
        {
            _remainingTime -= Time.deltaTime;
            OnHasBeenUpdated?.Invoke((_time - _remainingTime) / _time);
            yield return null;
        }

        OnTimeIsOver?.Invoke();
    }
}
