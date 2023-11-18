using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RacTools.Variables;
using RacTools.Miscelaneous;

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
    private RacTools.Utilities.Range HealthRange = new(0.5185f,0.48f);
    private void Start()
    {
        
        _bloodMaterial = bloodSphere.material;
       /* _soulMaterial = soulSphere.material;*/
    }

    private void Update()
    {

        _bloodAmount = ((float)health.Value / (float)_maxHealth.Value);
        Debug.Log(_bloodAmount);
        _bloodAmount = Map.MapFloatRange(_bloodAmount, RacTools.Utilities.Range.ZeroToOne, HealthRange);
        Debug.Log(_bloodAmount);
        _bloodMaterial.SetFloat("_LiquidAmount",_bloodAmount);

       /* _soulAmount = (souls.Value / _maxMoney) * 0.018f;
        _soulMaterial.SetFloat("_LiquidAmount", 0.508f - _soulAmount);*/

    }
}
