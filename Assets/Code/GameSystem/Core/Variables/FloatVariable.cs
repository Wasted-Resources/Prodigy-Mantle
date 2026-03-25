using UnityEngine;
// Float
[CreateAssetMenu(fileName = "New Float Variable", menuName ="Variables/Float Variable")]
public class FloatVariable : Variable<float>
{
    [SerializeField] private float _minValue = 0f ;
    [SerializeField] private float _maxValue = 9999f ;

    public float MinValue => _minValue ;
    public float MaxValue => _maxValue ;
    public override float ClampValue(float value) => Mathf.Clamp(value, _minValue, _maxValue) ;
}