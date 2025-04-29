using System.Collections;
using RacTools.Variables;
using UnityEngine;

namespace _VR_Helsing.Gun
{
    public class ChangeWeaponGunState : BaseGunState
    {
        private Variable<int> _magazine;
        private float _secondsToChangeWeapon;

        public ChangeWeaponGunState(GunStateMachine stateMachine, Variable<int> magazine) : base(stateMachine)
        {
            _magazine = magazine;
        }

        public override void Enter()
        {
            gunStateMachine.StartCoroutine(NextGunCoroutine());
        }

        public override void Exit()
        {
            
        }
        
        private IEnumerator NextGunCoroutine(float speedZ, float absSpeedLimit)
        {
            bool result = false;
            float seconds = 0f;
            float currentZSpeed = _zAxisBuffer.Sum(num => num);
            var speedSign = (int)Mathf.Sign(speedZ);

            while (seconds < secondsToChangeWeapon)
            {
                seconds += Time.deltaTime;
                //Si el signo de la veliciada actual es contrario a la velocidad inicial
                // y la velocidad absoluta de la velocidad actual es mayor al ,limite absoluto de velocidad
                // el resultado es posotivo
                if (((int)Mathf.Sign(currentZSpeed)) != speedSign && Mathf.Abs(currentZSpeed) > absSpeedLimit)
                {
                    result = true; break;
                }
                yield return null;

                currentZSpeed = _zAxisBuffer.Sum(num => num);
            }
        
            if(result)
                gunStateMachine.NextGun(speedSign);
        
        }
    }
}