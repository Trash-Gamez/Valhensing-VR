namespace RacTools.BehaviourTree
{
    public class RepeatNode : DecoratorNode
    {
        protected override void OnStart()
        {
            
        }

        protected override State OnUpdate()
        {
            // El nodo "RepeatNode" se basa en que si ignoras el estado del nodo hijo, y retornas que sigue corriendo
            // Entonces harás un loop
            Child.Update();
            return State.Running;
        }

        protected override void OnStop()
        {
            
        }
    }
}