#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/15
* Date: 2026-03-20
*/
#endregion


#region Basic Instruction
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
/// This script is redefined to Give access to stat serialization for variables that need to live on their instances.
/// </para>
/// <para>
/// </remarks>
/// <summary>
/// Description: A Serialization tool for Variables<T> if the reference of it is supposed to be run on several Instances of it.
/// Coordination: Automatically held in RuntimeStats.
/// Deployment: Just leave this here.
/// </summary>
#endregion


using System;

[Serializable]
public class StatInstance<T>
{
    public Variable<T> variableDefinition;
    public T Value;
    public Action<T> OnChanged;

    public StatInstance(Variable<T> varData)
    {
        variableDefinition = varData ;
        Value = varData.InitialValue ;
    }

    public void SetValue(T newValue)
    {
        Value = variableDefinition.ClampValue(newValue);
        OnChanged?.Invoke(Value) ;
    }
}