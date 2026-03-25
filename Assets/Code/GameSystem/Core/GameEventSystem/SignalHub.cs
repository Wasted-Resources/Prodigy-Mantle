#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/[ID]
* Date: 2026-03-25
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

[AddComponentMenu("WastedResources/EventSystem/Signal Hub")]
public class SignalHub : GameEventListenerBase
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [SerializeField] private List<GameEventResponse> _eventResponse = new();
    #endregion
    #region Internal

    protected override GameEventBase EventBase => null;
    #endregion


    #region Methods
    private void OnEnable()
    {
        foreach (var r in _eventResponse) r.Event.RegisterListener(this) ;
    }
    private void OnDisable()
    {
        foreach (var r in _eventResponse) r.Event.DeregisterListener(this) ;
    }
    public override void OnEventRaised(GameEventBase sender, object data)
    {
        if (data is GameObject target && (target == gameObject || target == transform.root.gameObject))
        {
            foreach (var r in _eventResponse)
            {
                if (r.Event == sender) r.Raise() ;
            }
        }
    }
    #endregion

}