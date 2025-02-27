using UnityEngine;

namespace _VanHelsingVR.Entities
{
    public abstract class EntityData : ScriptableObject
    {
        [SerializeField] private string entityName;
        [SerializeField] private int healthPoints;

        public string EntityName => entityName;
        public int HealthPoints => healthPoints;
    }

}