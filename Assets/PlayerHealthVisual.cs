
using System;
using RacTools.Variables;
using UniRx;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    public class PlayerHealthVisual : MonoBehaviour
    {
        [SerializeField] private Transform[] liquidObjects;
        [SerializeField] private Variable<int> maxHealth;
        [SerializeField] private Variable<int> currentHealth;

        private void CurrentHealthChanged(int newHealth)
        {
            var liquidValue = ((float)newHealth / (float)maxHealth.Value) * liquidObjects.Length;
            liquidValue = liquidValue <= 0 ? 0 : liquidValue;

            for (int i = 0; i < liquidObjects.Length; i++)
            {
                var scale = liquidObjects[i].localScale;
                scale.x = Mathf.Clamp01(liquidValue);
                liquidObjects[i].localScale = scale;
                liquidValue -= 1;
            }
        }

        private IDisposable _currentHealthSub;
        private void OnEnable()
        {
            _currentHealthSub = currentHealth.OnValueChanged
                .Subscribe(CurrentHealthChanged);
        }

        private void OnDisable()
        {
            _currentHealthSub.Dispose();
        }
    }
}
