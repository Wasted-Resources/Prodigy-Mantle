#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/13
* Date: 2026-03-16
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
/// This class handles referencing established variables for the game.
/// </para>
/// </remarks>
/// <summary>
/// Description: It references variables and defines if they are constant or not.
/// Coordination: Create a new Variable Reference within an object and assign the variable it refers to.
/// Deployment: It is an asset.
/// </summary>
#endregion


using System;
using UnityEngine;

#region Abstract class
public abstract class Reference<T, TVariable> where TVariable : Variable<T>
{
    [TextArea] public string Description ;
    [SerializeField] private bool _useConstant = true ;
    [SerializeField] private T _constantValue ;
    [SerializeField] private TVariable _variable ;
    public T Value => _useConstant ? _constantValue : _variable.Value ;
}
#endregion


#region References
// Float
[Serializable] public class FloatReference : Reference<float, FloatVariable> { }

// Integer
[Serializable] public class IntReference : Reference<int, IntegerVariable> { }

// Boolean
[Serializable] public class BoolReference : Reference<bool, BoolVariable> { }

// String
[Serializable] public class StringReference : Reference<string, StringVariable> { }

// Vector2
[Serializable] public class Vector2Reference : Reference<Vector2, Vector2Variable> { }

// Vector3
[Serializable] public class Vector3Reference : Reference<Vector3, Vector3Variable> { }


#endregion