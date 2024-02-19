using UnityEngine;
using _VanHelsingVR.Singletons;

namespace _VanHelsingVR.TickSystem
{
    public class TickSystem : PersistentMonoSingleton<TickSystem>
    {
        public const float MAX_TICK_TIME = 0.2f;

        public delegate void OnTickDel(int tick);
        public static event OnTickDel OnTick;

        public int Tick
        {
            get => _tick;
            set
            {
                _tick = value;
                OnTick?.Invoke(_tick);
            }
        }

        private int _tick;
        private float _currentTime;


        private void Update()
        {
            _currentTime += Time.deltaTime;
            if(_currentTime >= MAX_TICK_TIME)
            {
                _currentTime = MAX_TICK_TIME;
                Tick++;
            }
        }
    }
}
