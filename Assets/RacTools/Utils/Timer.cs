using System;
using System.Collections;
using UnityEngine;

namespace _VanHelsingVR.Utilities
{
    public class Timer : MonoBehaviour
    {
        [Min(0)]
        [SerializeField] private float seconds = 2f;
    
        [Space]
        [SerializeField] private bool initOnStart;

        [SerializeField] private bool restartOnPause = false;

        #region Events
        public event Action OnTimerEnded;
        public event Action OnTimerPaused;
        public event Action OnTimerResumed;
        public event Action OnTimerInitialized;
        #endregion
    
        private float _transcurredTime = 0f;

        public float TranscurredTime
        {
            get => _transcurredTime;
        }
    
        private Coroutine _timerCoroutine;
        private bool _isPaused;
        private void Start()
        {
            if(initOnStart)
                Initialize();
        }

        public void Initialize()
        {
            _timerCoroutine = StartCoroutine(TimerCor());
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
            _transcurredTime = 0;
        }

        public void Pause()
        {
            _isPaused = true;
            OnTimerPaused?.Invoke();
        
            if(restartOnPause)
                Restart();
        }
    
        private IEnumerator TimerCor()
        {
            while (_transcurredTime < seconds)
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
