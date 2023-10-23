using System;
using _VanHelsingVR.Extensions.UniRX;
using _VanHelsingVR.Text;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace _VanHelsingVR.Visualizers
{
    public abstract class VariableVizualizer<T> : MonoBehaviour
    {
        [InlineProperty] //TODO: Make only editor

        [SerializeField] private TextReference textReference;

        [SerializeField] private Variable<T> variable;
    
        [SerializeField] private bool useParamString;
        [SerializeField]
#if UNITY_EDITOR
        [ShowIf(nameof(useParamString))]
#endif
        private string paramString;

        [SerializeField, HideInPlayMode] private bool useSample;
        [SerializeField]
#if UNITY_EDITOR
        [HideInPlayMode, ShowIf(nameof(useSample)) ]
#endif 
        private float _sampleSeconds;
    
        protected virtual void Start()
        {
            var onValueChanged = variable.OnValueChanged;
            if (useParamString)
            {
                if (useSample)
                    onValueChanged = onValueChanged.Sample(TimeSpan.FromSeconds(_sampleSeconds));
            
                onValueChanged.SubscribeToTextRef(textReference, f =>
                {
                    var newString = string.Format(paramString, f.ToString());
                    return newString;
                }).AddTo(this);

                return;
            }

            if (useSample)
                onValueChanged = onValueChanged.Sample(TimeSpan.FromSeconds(_sampleSeconds));

            onValueChanged.SubscribeToTextRef(textReference)
                .AddTo(this);
        }

        protected virtual IObservable<T> GetObservable()
        {
            return variable.OnValueChanged;
        }
    }
}
