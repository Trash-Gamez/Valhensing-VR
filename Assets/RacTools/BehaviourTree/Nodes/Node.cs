using System.Collections.Generic;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            Running,
            Failure,
            Success
        }

        [HideInInspector] public string guid;
        [HideInInspector] public State state = State.Running;
        [HideInInspector] public bool started = false;
        [HideInInspector] public Vector2 pos; 
        
        public State Tick()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnTick();

            if (state == State.Failure || state == State.Success)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        public virtual Node Clone()
        {
            var node = Instantiate(this);
            return node;
        } 

        protected abstract void OnStart();
        protected abstract State OnTick();
        protected abstract void OnStop();

        public abstract void AddChild(Node child);
        public abstract void RemoveChild(Node child);
        public abstract List<Node> GetChildren();
    }
}
