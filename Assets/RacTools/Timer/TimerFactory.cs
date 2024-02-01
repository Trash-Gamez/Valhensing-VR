using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace RacTools.Timer
{
    public class TimerFactory
    {
        public MonoTimerHolder TimerHolder { get; private set; }

        public TimerFactory(MonoTimerHolder timerHolder)
        {
            Debug.Log("Timer Factory inicializado");
            TimerHolder = timerHolder;
        }
        
        public Timer Create(float seconds, bool initOnCreate = false)
        {
            return new Timer(seconds, TimerHolder, initOnCreate);
        }

        public Timer Create(float seconds, MonoBehaviour coroutineOwner, bool initOnCreate = false)
        {
            return new Timer(seconds, coroutineOwner, initOnCreate);
        }
    }
}
