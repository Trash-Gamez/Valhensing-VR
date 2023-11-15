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

        private Vector3 _targetDir;

        private void Start()
        {
            //Esto se hace para que el proyectil no seig aal jugador y solo siga la pocision del jugador de cuando fue creado el proyectil
            _targetDir = (target.position - transform.position).normalized;
        }

        void Update()
        {
            var newPos = transform.position + _targetDir * (Time.deltaTime * speed);
            var newRot = Quaternion.LookRotation(_targetDir);
            transform.SetPositionAndRotation(newPos, newRot);
        }
    }
}
