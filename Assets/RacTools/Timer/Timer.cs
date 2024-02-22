using System;
using System.Collections;
using UnityEngine;

namespace RacTools.Timer
{
    public class Timer
    {
        public bool InitOnStart => _initOnStart;
        private bool _initOnStart;
        
        private float _seconds = 2f;

        #region Events
        public event Action OnTimerEnded;
        public event Action OnTimerPaused;
        public event Action OnTimerResumed;
        public event Action OnTimerInitialized;
        #endregion
    
        private float _transcurredTime = 0f;

        public float TranscurredTime => _transcurredTime;

        private MonoBehaviour _coroutineOwner;
        private Coroutine _timerCoroutine;
        private bool _isPaused;


        public Timer(float seconds, MonoBehaviour coroutineOwner, bool initOnStart = false)
        {
            _seconds = seconds;
            _coroutineOwner = coroutineOwner;
            _initOnStart = initOnStart;
        }

        public void Initialize()
        {
            _timerCoroutine = _coroutineOwner.StartCoroutine(TimerCor());
            OnTimerInitialized?.Invoke();
        }

        public void Resume()
        {
            _isPaused = false;
        
            if(_timerCoroutine == null)
                Initialize();
        
            OnTimerResumed?.Invoke();
        }

        public void Restart()
        {
            Pause();
            _transcurredTime = 0;
            Resume();
        }

        public void Pause()
        {
            _isPaused = true;
            OnTimerPaused?.Invoke();
        }
    
        private IEnumerator TimerCor()
        {
            while (_transcurredTime < _seconds)
            {
                if(!_isPaused)
                    _transcurredTime += Time.deltaTime;
            
                yield return null;
            }
            OnTimerEnded?.Invoke();
            _timerCoroutine = null;
        }
    
    }
}
