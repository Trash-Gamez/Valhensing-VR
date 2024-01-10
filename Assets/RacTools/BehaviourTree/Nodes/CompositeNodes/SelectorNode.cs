using System;
using RacTools.Variables;

namespace RacTools.BehaviourTree
{
    public class SelectorNode : CompositeNode
    {
        private int _current;
        
        protected override void OnStart()
        {
            _current = 0;
        }

        protected override State OnTick()
        {
            var child = Children[_current];

            var childState = child.Tick();
            
            /*
             * Verificamos y devolvemos el mismo estado exceptuando en Failure, cuando un hijo termina
             * Significa que debemos pasar al siguiente hijo, asi que en los casos donde no se haya terminado
             * incorrectamente el estado, se retornara ese estado al BehaviourTree
             */
            switch (childState)
            {
                case State.Running:
                case State.Success:
                    return childState;
                case State.Failure:
                    _current++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
            //Si sobrepasamos el numero de hijos se termina la sequencia y retornamos el estado "Success"
            //Si no se retorrna Running y se sigue la secuencia
            return _current >= Children.Count ? State.Success : State.Running;
        }

        private State OnChildSucceed()
        {
            _current++;
            return State.Success;
        }

        protected override void OnStop()
        {
            
        }
    }
}