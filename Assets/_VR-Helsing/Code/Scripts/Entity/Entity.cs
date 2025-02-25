using UnityEngine;
using Sirenix.OdinInspector;

using _VanHelsingVR.Health;

namespace _VanHelsingVR.Entities
{
    [SelectionBase]
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected EntityData data;
        [SerializeField] protected HealthSystem healthSystem;

        [TitleGroup("Input")] [SerializeField] protected EntityInput input;

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {

        }

        protected virtual void Initialize()
        {

        }

        public virtual void SetEntityData(EntityData newData)
        {

        }
    }
}
