using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TEST_AdressablesLoader : MonoBehaviour
{
    [SerializeField] private AssetReferenceT<GameObject> _cityPrefab;
    [SerializeField] private AssetReference _cuadrantePrefab;
    [SerializeField] private AssetReference _cuadranteGrandePrefab;

    private GameObject _lastInstance;
    private async Task Instantiate()
    {
        Debug.Log("Iniciando carga");
        _lastInstance = await _cityPrefab.InstantiateAsync();
        Debug.Log("Terminando carga");
    }
    
    private void Unload(){
        _cityPrefab.ReleaseInstance(_lastInstance);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Unload();
        }
    }
    
}
