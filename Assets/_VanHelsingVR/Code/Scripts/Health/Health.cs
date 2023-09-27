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
        [field: SerializeField] public VariableReference<int> maxHealth { get; private set; }

        [Space] [SerializeField] private ConditionPool canBeDamaged;
        [SerializeField] private ConditionPool canBeHealed;
        
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onRecoverHealth;
        [SerializeField] private UnityEvent onDead;
        public int health{get; private set;}
        
        public void AddHealth(int addedLife)
        {
            if (addedLife == 0) return;

            if(addedLife < 0 && canBeDamaged.CanDo)
            {
                HandleDamage(addedLife);
            }

            if(addedLife > 0 && canBeDamaged)

            switch (addedLife)
            {
                case < 0:
                    HandleDamage(addedLife);
                    break;
                case > 0:
                    HandleHeal(addedLife);
                    break;
            }

            health = Mathf.Clamp(addedLife + health,0, maxHealth.Value);
        }

        protected virtual void HandleDamage(int damagedValue)
        {
            var damaged = health + damagedValue;

            if (damaged <= 0)
            {
                //TODO: milagro por parte del jugador, salvarse
                HandleDead();
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