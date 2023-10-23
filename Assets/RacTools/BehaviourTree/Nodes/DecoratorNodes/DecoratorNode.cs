namespace RacTools.BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        public Node Child { get; set; }
    }
}