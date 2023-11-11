using System;
using UnityEngine;

namespace _VanHelsingVR.Proyectile
{
    public class SimpleProyectile : MonoBehaviour
    {
        [HideInInspector]
        public float speed;
        [HideInInspector]
        public Transform target;

        private Vector3 _targetPos;

        private void Start()
        {
            //Esto se hace para que el proyectil no seig aal jugador y solo siga la pocision del jugador de cuando fue creado el proyectil
            _targetPos = target.position;
        }

        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);
            transform.LookAt(_targetPos);
        }
    }
}
