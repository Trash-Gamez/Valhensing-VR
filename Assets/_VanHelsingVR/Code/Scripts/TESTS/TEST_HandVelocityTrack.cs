using System.Collections;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _VanHelsingVR.TESTS
{
    public class TEST_HandVelocityTrack : MonoBehaviour
    {
        [SerializeField]
        private FloatVariable _yPos;

        [SerializeField] 
        private FloatVariable _ySpeed;

        [SerializeField] 
        private FloatVariable _yDelta;

        [SerializeField] 
        private IntVariable _reloadedTimes;
    
        [SerializeField]
        private bool useLocalRot = false;

        [SerializeField, Min(0.001f)]
#if UNITY_EDITOR
        [BoxGroup("Realod Config")]
#endif
        private float _speedToReload;
    
        [SerializeField]
#if UNITY_EDITOR
        [BoxGroup("Relaod Config")]
#endif
        private float _reloadCadence;
    
        private Coroutine _reloadCor = null;

        public enum WayToDoIt
        {
            First,
            Second
        }

        [SerializeField] private WayToDoIt _wayToDoIt;

        private void Start()
        {
            _reloadedTimes.Value = 0;
        }

        void Update()
        {
            switch (_wayToDoIt)
            {
                case WayToDoIt.First:
                    FirstWayToReload();
                    break;
                case WayToDoIt.Second:
                    break;
            }
        }
    
        #region FirstReload
        private void FirstWayToReload()
        {
            var newPos = transform.localPosition;
            float newYPos = 0;
        
            if (useLocalRot)
                newPos = transform.rotation * newPos;

            newYPos = newPos.y;
                
        

            _yDelta.Value = newYPos - _yPos.Value;

            _ySpeed.Value = _yDelta.Value / Time.deltaTime;
        
            _yPos.Value = newYPos;

            if (Mathf.Abs(_ySpeed.Value) < _speedToReload) return;
            if (_reloadCor != null) return;
        
            _reloadCor = StartCoroutine(FirstReloadCor());
        }

        IEnumerator FirstReloadCor()
        {
            _reloadedTimes.Value++;
            yield return new WaitForSeconds(_reloadCadence);
            _reloadCor = null;
        }
        #endregion
    
        #region SecondReload

        private void SecondWayToReload()
        {
        
        }

        #endregion
    
    }
}
