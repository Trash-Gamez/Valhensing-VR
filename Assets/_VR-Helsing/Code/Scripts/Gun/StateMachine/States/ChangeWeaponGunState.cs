using System.Collections;
using RacTools.Variables;
using UnityEngine;

namespace _VR_Helsing.Gun
{
    public class ChangeWeaponGunState : BaseGunState
    {
        private Variable<int> _magazine;
        private Variable<Vector3> _localAngularVelocity;
        private float _secondsToChangeWeapon;

        public ChangeWeaponGunState(GunStateMachine stateMachine, GunStateFactory factory) : base(stateMachine, factory)
        {
            _localAngularVelocity = GunStateMachine.LocalAngularVelocity;
            _magazine = GunStateMachine.Magazine;
        }

        public override void Enter()
        {
            GunStateMachine.StartCoroutine(NextGunCoroutine());
        }

        public override void Exit()
        {
            
        }
        
        private IEnumerator NextGunCoroutine(float speedZ, float absSpeedLimit)
        {
            bool result = false;
            float seconds = 0f;
            float currentZSpeed = GunStateMachine.ZLocalAngVelBuffer.Average();
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
                GunStateMachine.NextGun(speedSign);
        
        }
    }
}