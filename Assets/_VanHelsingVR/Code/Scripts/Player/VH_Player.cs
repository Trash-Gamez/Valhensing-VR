using System;
using Autohand;
using UnityEngine;

namespace _VanHelsingVR.Player
{
    public class VH_Player : MonoBehaviour
    {
        [SerializeField] private AutoHandPlayer autoHandPlayer;
        
        

        #region EDITOR
        #if UNITY_EDITOR
        private void OnValidate()
        {
            autoHandPlayer = GetComponentInChildren<AutoHandPlayer>();
        }
        #endif
        #endregion
    }
}
