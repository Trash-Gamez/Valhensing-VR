using System.Collections.Generic;
using UnityEngine;

namespace _VanHelsingVR.Entities
{
    
        [CreateAssetMenu(fileName = "EntityDataListSO", menuName = "Scriptable Objects/EntityDataListSO")]

    public class EntityDataListSO : ScriptableObject
    {
        [field: SerializeField] public List<EntityData> EntitieDataList { get; private set; }

        public EntityData PickRandom()
        {
            return EntitieDataList.PickRandom();
        }
    }

}