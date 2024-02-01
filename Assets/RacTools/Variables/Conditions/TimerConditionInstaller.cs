using RacTools.Timer;
using RacTools.Utils;
using UnityEngine;
using Zenject;

namespace _VanHelsingVR.Conditions
{
    public class TimerConditionInstaller : MonoInstaller
    {
        [SerializeField] private Timer timer;
    
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeCondition>()
                .AsSingle();
        
            Container.Bind<Timer>().FromInstance(timer);
        }
    }
}