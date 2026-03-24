using System;
using UnityEngine;
// Float
[CreateAssetMenu(fileName = "New Float Variable", menuName ="Variables/Float Variable")]
public class FloatVariable : Variable<float>
{
    [SerializeField] private float _minValue = 0f ;
    [SerializeField] private float _maxValue = 9999f ;

    public float MinValue => _minValue ;
    public float MaxValue => _maxValue ;
    public float ClampedInitialValue => Mathf.Clamp(InitialValue, _minValue, _maxValue) ;
}