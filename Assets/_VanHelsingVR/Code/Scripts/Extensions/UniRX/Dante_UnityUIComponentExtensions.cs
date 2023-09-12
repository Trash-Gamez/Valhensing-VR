using System;
using UniRx;

namespace _VanHelsingVR.Extensions.UniRX
{
    public static partial class UnityUIComponentExtensions
    {
        public static IDisposable SubscribeToTextRef(this IObservable<string> source, TextReference text)
        {
            return source.SubscribeWithState(text, (x, t) => t.Value = x);
        }

        public static IDisposable SubscribeToTextRef<T>(this IObservable<T> source, TextReference text)
        {
            return source.SubscribeWithState(text, (x, t) => t.Value = x.ToString());
        }
        
        public static IDisposable SubscribeToTextRef<T>(this IObservable<T> source, TextReference text, Func<T, string> selector)
        {
            return source.SubscribeWithState2(text, selector, (x, t, s) => t.Value = s(x));
        }
    }
}
