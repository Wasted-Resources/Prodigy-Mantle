#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/[ID]
* Date: 2026-03-31
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


public class LocomotivePattern : MonoBehaviour, ISetDirection, IImpulse
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [SerializeField] private FloatReference _moveSpeed ;
    [SerializeField] private FloatReference _acceleration ;
    [SerializeField] private FloatReference _maxSpeed ;
    [SerializeField] private BoolReference _useGravity ;
    #endregion


    #region Internal
    private IMotionBody _body ;
    private Vector3 _inputDir ;
    private Vector3 _impulse ;
    private float _verticalVelocity ;
    #endregion


    #region Methods
    void Awake()
    {
        _body = GetComponent<IMotionBody>();
    }

    public void SetMoveDirection(Vector3 direction){        Debug.Log($"3. Locomotive: Intent Received {direction}");
 _inputDir = direction; }

    public void ApplyForce(Vector3 force) => _impulse += force ;

    private void Update()
    {
        Debug.DrawRay(transform.position, _inputDir * 20, Color.green);
        if (_useGravity.Value)
        {
            if (_body.IsGrounded && _verticalVelocity <0) _verticalVelocity = -2f;
            else _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity = Mathf.Lerp(_verticalVelocity, 0, Time.deltaTime * _acceleration.Value );
        }

        Vector3 targetVelocity = _inputDir * _moveSpeed.Value ;
        Vector3 finalVelocity = targetVelocity + _impulse ;
        finalVelocity.y = _verticalVelocity ;

        _body.Move(finalVelocity) ;

        _impulse = Vector3.Lerp(_impulse, Vector3.zero, Time.deltaTime * _acceleration.Value);
    }
    #endregion
}