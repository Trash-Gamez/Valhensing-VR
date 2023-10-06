using UnityEngine;
using Zenject;

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