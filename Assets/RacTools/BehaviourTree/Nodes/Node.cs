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

        [HideInInspector] public State state = State.Running;
        [HideInInspector] public bool started = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 pos; 
        
        public State Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

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
        protected abstract State OnUpdate();
        protected abstract void OnStop();

        public abstract void AddChild(Node child);
        public abstract void RemoveChild(Node child);
        public abstract List<Node> GetChildren();
    }
}
