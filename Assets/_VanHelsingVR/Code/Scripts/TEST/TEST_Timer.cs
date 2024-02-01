using System;
using System.Collections;
using System.Collections.Generic;
using RacTools.Timer;
using TMPro;
using UnityEngine;
using Zenject;

public class TEST_Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public float secondsToWait;

    private TimerFactory _timerFactory;
    private Timer _myTimer;
    private bool _isRunning = false;
    
    [Inject]
    public void Init(TimerFactory timerFactory)
    {
        Debug.Log("Se ha inyectado");
        _timerFactory = timerFactory;
        
        _myTimer = _timerFactory.Create(secondsToWait,this,  true);
        _myTimer.OnTimerEnded += TimerOnOnTimerEnded;
        _myTimer.OnTimerPaused += TimerOnOnTimerPaused;
        _isRunning = true;
    }

    private void TimerOnOnTimerPaused()
    {
        _isRunning = false;
    }

    private void TimerOnOnTimerEnded()
    {
        _isRunning = false;
        text.text = "0";
    }

    private void Update()
    {
        if (!_isRunning) return;
        text.text = _myTimer.TranscurredTime.ToString();
    }
}
