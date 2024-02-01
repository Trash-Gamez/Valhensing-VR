using UnityEngine;
using Zenject;

namespace RacTools.Timer
{
    public class TimerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TimerFactory>()
                .FromMethod(SelectTimerFactory)
                .AsCached();
        }

        private TimerFactory SelectTimerFactory(InjectContext context)
        {
            Debug.Log("Creando en el instalador: " + context.MemberName);
            var monoTimerHolder = new GameObject("--Timers Holder--").AddComponent<MonoTimerHolder>();
            return new TimerFactory(monoTimerHolder);
        }
    }
}
