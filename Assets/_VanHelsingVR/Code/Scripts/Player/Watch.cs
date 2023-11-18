using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RacTools.Variables;
public class Watch : MonoBehaviour
{
    [SerializeField] MeshRenderer bloodSphere;
    [SerializeField] MeshRenderer soulSphere;

    [SerializeField] VariableReference<int> health;
    [SerializeField] VariableReference<int> souls;

    private Material _bloodMaterial;
    private Material _soulMaterial;

    [SerializeField] VariableReference<int> _maxHealth;
    private float _bloodAmount;
    private float _maxMoney = 999;
    private float _soulAmount;
    private void Start()
    {
        
        _bloodMaterial = bloodSphere.material;
        _soulMaterial = soulSphere.material;
    }

    private void Update()
    {
        _bloodAmount = (health.Value / _maxHealth.Value) *0.038f ;
        _bloodMaterial.SetFloat("_LiquidAmount",0.5185f - _bloodAmount);

        _soulAmount = (souls.Value / _maxMoney) * 0.018f;
        _soulMaterial.SetFloat("_LiquidAmount", 0.508f - _soulAmount);

    }
}
