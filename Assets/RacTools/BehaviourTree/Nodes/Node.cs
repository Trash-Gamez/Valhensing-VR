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

        public State state = State.Running;
        public bool started = false;
        public string guid;
        public Vector2 pos; 
        
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

        protected abstract void OnStart();
        protected abstract State OnUpdate();
        protected abstract void OnStop();

        public abstract void AddChild(Node child);
        public abstract void RemoveChild(Node child);
        public abstract List<Node> GetChildren();
    }
}
