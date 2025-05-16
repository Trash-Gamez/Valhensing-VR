using _VanHelsingVR.Explosion;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DoExplosion", story: "Make [Explosion]", category: "Action", id: "7420ac1eae33a823fb83bee1773af5b4")]
public partial class DoExplosionAction : Action
{
    [SerializeReference] public BlackboardVariable<Explosion> Explosion;

    protected override Status OnStart()
    {
        Explosion.Value.DoExplosion();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

