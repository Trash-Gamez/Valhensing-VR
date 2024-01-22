using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TEST_AdressablesLoader : MonoBehaviour
{
    [SerializeField] private AssetReferenceT<GameObject> _cityPrefab;
    [SerializeField] private AssetReference _cuadrantePrefab;
    [SerializeField] private AssetReference _cuadranteGrandePrefab;

    private async void Start()
    {
        
    }
}
