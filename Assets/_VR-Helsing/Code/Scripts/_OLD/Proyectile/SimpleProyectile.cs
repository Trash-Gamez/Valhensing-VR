using System;
using _VanHelsingVR.Health;
using UnityEngine;

namespace _VanHelsingVR.Proyectile
{
    public class SimpleProyectile : MonoBehaviour
    {
        [HideInInspector]
        public float speed;
        [HideInInspector]
        public Transform target;
        [HideInInspector]
        public Transform from;

        [Min(1),SerializeField] private float deflectSpeedMultiplier = 2;

        private Vector3 _targetDir;
        private bool _isDeflected;

        [SerializeField]
        private Hitbox proyectileHitbox;

        private float _secondsForDestroy = 10;

        private void Start()
        {
            //Esto se hace para que el proyectil no seig aal jugador y solo siga la pocision del jugador de cuando fue creado el proyectil
            _targetDir = ((target.position+new Vector3(0,1.5f,0)) - transform.position).normalized;
            
            Invoke(nameof(DestroyObject), _secondsForDestroy);
        }

        void Update()
        {
            if (_isDeflected)
            {
                if (!from)
                {
                    CancelInvoke(nameof(DestroyObject));
                    this.enabled = false;
                    Destroy(gameObject);
                    return;
                }
                _targetDir = (from.position - transform.position).normalized;
            }
            var newPos = transform.position + _targetDir * (Time.deltaTime * speed);
            var newRot = Quaternion.LookRotation(_targetDir);
            transform.SetPositionAndRotation(newPos, newRot);
        }

        public void Deflect()
        {
            CancelInvoke(nameof(DestroyObject));

            speed *= deflectSpeedMultiplier; 
            _isDeflected = true;
            proyectileHitbox.ChangeTeam(DamageTeam.Player);
            proyectileHitbox.AddHitBoxData(HitBoxDataType.Damage, new HitBoxDataAttribute(){ IntValue = 99999}); //Instakill
            
            Invoke(nameof(DestroyObject), _secondsForDestroy);
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
