using System.Collections;
using _VanHelsingVR.Animation.Gun;
using RacTools.Variables;
using UnityEngine;

namespace _VR_Helsing.Gun
{
    public class ChangeWeaponGunState : BaseGunState
    {
        private Variable<int> _magazine;
        private GunInput _input; 
        private float _maxSecondsToChangeWeapon;

        private int _initialZVelSign;
        private Coroutine _checkChangeGunCor = null;
        private bool _result, _ended;

        public ChangeWeaponGunState(GunStateMachine stateMachine, GunStateFactory factory) : base(stateMachine, factory)
        {
            _magazine = GunStateMachine.Magazine;
            _input = GunStateMachine.Input;
            _maxSecondsToChangeWeapon = GunStateMachine.MaxSecondsToChangeWeapon;
        }

        public override void Enter()
        {
            _ended = false;
            var speedz = GunStateMachine.ZLocalAngVelBuffer.Average();
            _initialZVelSign = (int)Mathf.Sign(speedz);
            GunStateMachine.StartCoroutine(NextGunCoroutine(_initialZVelSign));
        }

        public override void Exit()
        {
            
        }

        public override void CheckState()
        {
            if (!_input.IsGripping)
            {
                if(_checkChangeGunCor != null) GunStateMachine.StopCoroutine(_checkChangeGunCor);
                GunStateMachine.ChangeState(StateFactory.IdleState);
                return;
            }

            if (!_ended) return;
            
            if(_result)
                GunStateMachine.NextGun(_initialZVelSign);
        }

        private IEnumerator NextGunCoroutine(int initialZVelSign)
        {
            float seconds = 0f;
            float currentZSpeed = GunStateMachine.ZLocalAngVelBuffer.Average();

            while (seconds < _maxSecondsToChangeWeapon)
            {
                seconds += Time.deltaTime;
                var currentVelSign = ((int)Mathf.Sign(currentZSpeed));
                //Si el signo de la veliciada actual es contrario a la velocidad inicial
                // y la velocidad absoluta de la velocidad actual es mayor al ,limite absoluto de velocidad
                // el resultado es posotivo
                if (currentVelSign != initialZVelSign && Mathf.Abs(currentZSpeed) > GunStateMachine.ZAngularVelocityThreshold)
                {
                    _result = true;
                    _ended = true;
                    break;
                }
                yield return null;

                currentZSpeed = GunStateMachine.ZLocalAngVelBuffer.Average();
            }

            _ended = true;
        }
    }
}