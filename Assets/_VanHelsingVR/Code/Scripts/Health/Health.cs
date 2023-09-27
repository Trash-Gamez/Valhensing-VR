using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _VanHelsingVR.Variables;
using _VanHelsingVR.Events;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _VanHelsingVR.Health
{
    public class Health : MonoBehaviour
    {
        [Title("Health Config")]
        [SerializeField] private Variable<int> health;
        [SerializeField] private Variable<int> maxHealth;

        [Space] [SerializeField] private ConditionPool canBeDamaged;
        [SerializeField] private ConditionPool canBeHealed;
        
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onRecoverHealth;
        [SerializeField] private UnityEvent onDead;
        
        public void AddHealth(int addedLife)
        {
            switch (addedLife)
            {
                case < 0:
                    HandleDamage(addedLife);
                    break;
                case > 0:
                    HandleHeal(addedLife);
                    break;
            }

            health.Value = Mathf.Clamp(addedLife + health.Value,0, maxHealth.Value);
        }

        protected virtual void HandleDamage(int damagedValue)
        {
            var damaged = health.Value + damagedValue;

            if (damaged <= 0)
            {
                //TODO: milagro por parte del jugador, salvarse
                
            }
        }

        protected virtual void HandleHeal(int healValue)
        {
            onRecoverHealth.Invoke();

        }

        protected virtual void HandleDead()
        {
            onDead.Invoke();
        }
    }
}