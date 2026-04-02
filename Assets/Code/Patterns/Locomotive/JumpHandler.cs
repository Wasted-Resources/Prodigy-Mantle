#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/[ID]
* Date: 2026-04-02
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
using UnityEngine;


public class JumpHandler : MonoBehaviour
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty;
#endif
    [SerializeField] private FloatReference _jumpForce; 
    #endregion

    #region Internal
    private IImpulse _motor;
    private IMotionBody _body;
    #endregion

    #region Methods
    private void Awake()
    {
        _motor = GetComponent<IImpulse>();
        _body = GetComponent<IMotionBody>(); //
    }

    public void Execute(GameObject sender)
    {
        if (_body != null && _body.IsGrounded)
        {
            _motor?.ApplyForce(Vector3.up * _jumpForce.Value);
        }
    }
    #endregion
}