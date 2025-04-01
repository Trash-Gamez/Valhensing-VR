using System;
using System.Collections;
using _VanHelsingVR.Health;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace _VanHelsingVR.Proyectile
{
    public class ThrowingProyectile : MonoBehaviour
    {
        [Min(1),SerializeField] private float deflectSpeedMultiplier = 2;
        [SerializeField] private Hitbox proyectileHitbox;
        private Transform _from, _target;
        private float _speed;
        
        private float _secondsForDestroy = 10;


        public void Init(Transform from, Transform target, float speed, MonoBehaviour onDestroyListener)
        {
            _from = from;
            _target = target;
            _speed = speed;
            onDestroyListener.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    Destroy(gameObject);
                })
                .AddTo(this);
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
            
            Destroy(gameObject);
            _throwCoroutine = null;
        }
        
        private IEnumerator ThrowCor(Vector3 startPos, Transform endPos)
        {
            transform.SetParent(null, true);

            float t = 0;
            

            while (t < 1)
            {
                if (!endPos)
                {
                    StopCoroutine(_throwCoroutine);
                    Destroy(gameObject);
                }
                float distance = Vector3.Distance(startPos, endPos.position);
                float finalSpeed = distance / _speed;

                t += Time.deltaTime / finalSpeed;

                var y = Mathf.Sin(Mathf.PI * Mathf.Lerp(0, 180, t) / 360) * distance;
                var finalPos = startPos;
                finalPos.y += y;

                transform.position = Vector3.Lerp(finalPos, endPos.position, t);
                yield return null;
            }
            
            Destroy(gameObject);
            _throwCoroutine = null;
        }

        public void Deflect()
        {
            if(_throwCoroutine != null)
                StopCoroutine(_throwCoroutine);

            _speed *= deflectSpeedMultiplier;

            //_target = _from;
            proyectileHitbox.ChangeTeam(DamageTeam.Player);
            proyectileHitbox.AddHitBoxData(HitBoxDataType.Damage, new HitBoxDataAttribute(){ IntValue = 99999}); //Instakill
            _throwCoroutine = StartCoroutine(ThrowCor(transform.position, _from));
        }

        private void OnDestroy()
        {
            if(_throwCoroutine != null)
                StopCoroutine(_throwCoroutine);
        }
    }
}