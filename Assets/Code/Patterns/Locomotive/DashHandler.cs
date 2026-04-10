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


using UnityEngine;


public class DashHandler : MonoBehaviour
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [SerializeField] private FloatReference _dashPower; 
    [SerializeField] private Vector2Reference _moveInput;
    [SerializeField] private Vector3Reference _cameraForward;
    [SerializeField] private BoolReference _isDashing ;
    [SerializeField] private FloatReference _dashCooldown ;
    #endregion


    #region Internal
    private IImpulse _motor;
    private float _nextDashTime ;
    #endregion


    #region Methods
    private void Awake() => _motor = GetComponent<IImpulse>();
    void OnEnable()
    {
        _isDashing.Variable.OnValueChanged += HandleInput ;
    }
    void OnDisable()
    {
        _isDashing.Variable.OnValueChanged -= HandleInput ;
    }
    private void HandleInput(bool isPressed)
    {
        if (isPressed && Time.time >= _nextDashTime)
        {
            Execute();
            _nextDashTime = Time.time + _dashCooldown.Value;
        }
    }
    public void Execute()
    {
        Vector3 look = _cameraForward.Value;
        Vector3 moveForward = new Vector3(look.x, 0, look.z).normalized;
        Vector3 moveRight = Vector3.Cross(Vector3.up, moveForward);

        Vector3 moveDir = (moveForward * _moveInput.Value.y) + (moveRight * _moveInput.Value.x);

        if (moveDir.sqrMagnitude < 0.01f)
        {
            moveDir = transform.forward;
        }

        _motor?.ApplyForce(moveDir.normalized * _dashPower.Value);
    }
    #endregion
}