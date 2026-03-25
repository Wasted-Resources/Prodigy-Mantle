#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/16
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
/// This class handles synchronization of a registry and the folder that i usually sort things into already. It exists because it automates the process.
/// Only problem I can see come up, may be with Stats that are deprecated and either need to be manually moved or reviewed.
/// </para>
/// </remarks>
/// <summary>
/// Description: Editor class that adds a synchronization function to a Registry.
/// Coordination: Drag and Drop the folder in which a given type of definition needs to live synchronize with the Button.
/// Deployment: Lives in the Editor Folder. A Developer tool only.
/// </summary>
#endregion

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Registry<>), true)]
public class RegistryEditor : Editor
{
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var registry = target as dynamic ;
        if (GUILayout.Button("Synchronize with Folder")) registry.PopulateFromFolder();
    }
}