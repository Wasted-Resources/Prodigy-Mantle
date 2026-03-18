#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/13
* Date: 2026-03-16
* As sources to the structure mof this class, please watch https://www.youtube.com/watch?v=raQ3iHhE_Kk for introduction
* Further sources: https://unity.com/how-to/architect-game-code-scriptable-objects, https://github.com/unity-atoms/unity-atoms, https://unity-atoms.github.io/unity-atoms/docs/
*/

/*
Structure the class into regions as appropriate for their use case.
The regions should separate what is viewed or used in an inspector, class intern relevant fields, Public Getters if necessary, 
Use top comments above methods to describe them and explain their parameters.
TODO comments above a method or codeblock
Use side comments in line to describe lines that obfuscate their function as explanation
*/
#endregion


#region Development remarks
/// <remarks>
/// <para>
/// This is the abstract class generic base class for all SO-based Variables in the system.
/// It must never be instantiated directly, only through concrete typed subclass.
/// </para>
/// </remarks>
/// <summary>
/// Description: Abstract generic base defining the shared characteristics of all Variable assets.
/// Coordination: Subclass this for each required type. Typical variables for most system will be included.
/// Deployment: Not instantiated directly, Concrete subclasses are the actual assets.
/// </summary>
#endregion

using System;
using UnityEngine;


public abstract class Variable<T> : ScriptableObject, ISerializationCallbackReceiver
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [SerializeField] private T _initialValue ;
    public T InitialValue => _initialValue ;

    #endregion


    #region Runtime
    private T _runtimeValue ;
    public T Value
    {
        get => _runtimeValue;
        set => SetValue(value);
    }
    public event Action<T> OnValueChanged ;
    #endregion


    #region Methods
    public void OnAfterDeserialize() => _runtimeValue = _initialValue ;

    public void OnBeforeSerialize() { } // Do nothing

    public void ResetToInitial() => SetValue(_initialValue);
    private void SetValue(T value)
    {
        _runtimeValue = value ;
        OnValueChanged?.Invoke(_runtimeValue) ;
    }
    #endregion

}

#region Variable Definitions
// Float
[CreateAssetMenu(fileName = "New Float Variable", menuName ="Variables/Float Variable")]
public class FloatVariable : Variable<float> { }

// Integer
[CreateAssetMenu(fileName = "New Integer Variable", menuName = "Variables/Integer Variable")]
public class IntegerVariable : Variable<int> { }

// Boolean
[CreateAssetMenu(fileName = "New Bool Variable", menuName = "Variables/Bool Variable")]
public class BoolVariable : Variable<bool> { }

// String
[CreateAssetMenu(fileName ="New Sting Variable", menuName ="Variables/String Variable")]
public class StringVariable : Variable<string> { }

// Vector2
[CreateAssetMenu(fileName ="New Vector2 Variable", menuName ="Variables/Vector2 Variable")]
public class Vector2Variable : Variable<Vector2> { }

// Vector 3
[CreateAssetMenu(fileName ="New Vector3 Variable", menuName ="Variables/Vector3 Variable")]
public class Vector3Variable : Variable<Vector3> { }

#endregion