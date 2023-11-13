using System;
using System.Collections;
using UnityEngine;

namespace _VanHelsingVR.Proyectile
{
    public class ThrowingProyectile : MonoBehaviour
    {
        private Transform _target;
        private float _speed;

        public void Init(Transform target, float speed)
        {
            _target = target;
            _speed = speed;
        }
        
        public void Throw()
        {
            if (_throwCoroutine != null) return;
            _throwCoroutine = StartCoroutine(ThrowCor(transform.position, _target.position));
        }

        private Coroutine _throwCoroutine = null;
        private IEnumerator ThrowCor(Vector3 startPos, Vector3 endPos)
        {
            transform.SetParent(null, true);

            float t = 0;
            float distance = Vector3.Distance(startPos, endPos);
            float finalSpeed = distance / _speed;

            while (t < 1)
            {
                t += Time.deltaTime / finalSpeed;

                var y = Mathf.Sin(Mathf.PI * Mathf.Lerp(0, 180, t) / 360) * distance;
                var finalPos = startPos;
                finalPos.y += y;

                transform.position = Vector3.Lerp(finalPos, endPos, t);
                yield return null;
            }
        }
    }
}