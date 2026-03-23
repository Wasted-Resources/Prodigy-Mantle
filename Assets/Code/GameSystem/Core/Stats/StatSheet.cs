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
/// This class handles [Core Responsibility]. It must maintain [Architecture Constraint, e.g., Singleton].
/// </para>
/// </remarks>
/// <summary>
/// Description: [Describe what this class does].
/// Coordination: [How it communicates with APIs or other Components].
/// Deployment: [Where it should live in the Scene, Project, Assets'].
/// </summary>
#endregion

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New StatSheet", menuName ="Configurations/Stat Sheet")]
public class StatSheet : ScriptableObject
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [TextArea][SerializeField] private string _description ;
    [SerializeField] private List<StatEntry> _stats = new() ;

    #endregion


    #region Public Getters
    public string Description => _description ;
    public float Get(string statName, float fallback = 0f)
    {
        foreach (var entry in _stats)
            if (entry.Name == statName) return entry.Value ;

        return fallback ;
    }
    public bool TryGet(string statName, out float value)
    {
        foreach (var entry in _stats)
        {
            if (entry.Name == statName)
            {
                value = entry.Value ;
                return true ;
            }
        }
        value = 0f ;
        return false ;
    }
    #endregion
}

[Serializable]
public class StatEntry
{
    #region Inspector
    [SerializeField] private string _name ;
    [SerializeField] private float _value ;

    public string Name => _name ;
    public float Value => _value ;
    #endregion
}