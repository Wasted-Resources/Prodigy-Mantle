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
    [SerializeField] private BoolReference _isGrounded ;
    [SerializeField] private Transform _visualRoot;
    #endregion


    #region Internal
    private IMotionBody _body ;
    private Vector3 _inputDir ;
    private Vector3 _impulse ;
    private float _verticalVelocity ;
    private float _inputLockoutTimer;
    #endregion


    #region Methods
    void Awake()
    {
        _body = GetComponent<IMotionBody>() ?? GetComponentInChildren<IMotionBody>() ?? GetComponentInParent<IMotionBody>();
    }

    public void SetMoveDirection(Vector3 direction)=> _inputDir = direction;

    public void ApplyForce(Vector3 force)
    {
        if (force.y != 0)
            _verticalVelocity = force.y ;
        if (new Vector2(force.x, force.z).magnitude > 0.01f)
        {
            _inputLockoutTimer = Time.time + 0.2f;
        }
        _impulse += new Vector3(force.x, 0 , force.z) ;
    }

    private void Update()
    {
        bool isLockedOut = Time.time < _inputLockoutTimer;
        Vector3 Input = isLockedOut ? Vector3.zero : _inputDir ;

        if (Input.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_inputDir.normalized);
            _visualRoot.rotation = Quaternion.Slerp(_visualRoot.rotation, targetRotation, Time.deltaTime*10f);
        }
        if (_useGravity.Value)
        {
            if (_isGrounded.Value && _verticalVelocity <0)
                _verticalVelocity = -2f;
            else 
                _verticalVelocity += Physics.gravity.y * 3f * Time.deltaTime; // Bandaid since CC and gravity hate each other. It only fuels my utter contempt for this component
        }

        Vector3 finalVelocity = (Input * _moveSpeed.Value) + _impulse ;
        finalVelocity.y = _verticalVelocity ;

        _body.Move(finalVelocity) ;

        _impulse = Vector3.Lerp(_impulse, Vector3.zero, Time.deltaTime * _acceleration.Value);
        if (_impulse.sqrMagnitude < 0.001f) _impulse = Vector3.zero;
    }
    #endregion
}