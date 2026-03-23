#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/11
* Date: 2026-03-23
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
/// Lightweight interface implemented by any ScriptableObject asset that lives in a registry.
/// </para>
/// </remarks>
/// <summary>
/// Deployment: Applied to SO definition classes.
/// </summary>
#endregion



public interface INamedAsset
{
    string AssetName { get ; }
    TypeSO AssetType { get ; }
}