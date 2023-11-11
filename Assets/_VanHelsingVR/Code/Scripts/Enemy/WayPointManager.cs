using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class WayPointManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> wayPoints = new List<Transform>();

        private int _current = -1;
        
        public Transform GetNext()
        {
            if(wayPoints == null) throw new NullReferenceException();
            if (!wayPoints.Any()) throw new ArgumentNullException();
            
            if (++_current >= wayPoints.Count)
            {
                _current = 0;
            }

            return wayPoints[_current];
        }

        public Transform GetFirst()
        {
            if(wayPoints == null) throw new NullReferenceException();
            if (!wayPoints.Any()) throw new ArgumentNullException();

            return wayPoints[0];
        }
        
        public void Restart()
        {
            _current = -1;
        }
    }
}