using UnityEngine;
using UnityEngine.Events;

namespace _VanHelsingVR.Events
{
    public sealed class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;

        [SerializeField] private UnityEvent callback;

        private void OnEnable()
        {
            gameEvent.AddListener(this);
        }

        private void OnDisable()
        {
            gameEvent.RemoveListener(this);
        }

        public void Raise()
        {
            callback.Invoke();
        }
    }
}
