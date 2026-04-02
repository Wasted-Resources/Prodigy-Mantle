#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/[ID]
* Date: 2026-04-01
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


using UnityEngine;

[RequireComponent(typeof(IMotionBody))]
public class MovementHandler : MonoBehaviour
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [SerializeField] private Vector2Reference _moveInput ;
    [SerializeField] private Vector3Reference _cameraForward ;
    
    #endregion
    #region Internal
    private ISetDirection _receiver;
    void Awake() => _receiver = GetComponent<ISetDirection>();
    #endregion


    #region Methods
    public void Execute(GameObject sender)
    {
        Vector3 lookDir = _cameraForward.Value ;
        Vector3 moveForward = new Vector3(lookDir.x, 0 ,lookDir.z).normalized ;
        Vector3 moveRight = Vector3.Cross(Vector3.up, moveForward) ;
        Vector3 moveDir = (moveForward * _moveInput.Value.y) + (moveRight * _moveInput.Value.x) ;
        _receiver.SetMoveDirection(moveDir) ;
    }
    #endregion
}