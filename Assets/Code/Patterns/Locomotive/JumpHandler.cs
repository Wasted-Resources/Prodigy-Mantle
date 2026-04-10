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


public class JumpHandler : MonoBehaviour
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty;
#endif
    [SerializeField] private BoolReference _isJumping ;
    [SerializeField] private BoolReference _isGrounded ;
    [SerializeField] private BoolReference _isWallrunning ;
    [SerializeField] private FloatReference _jumpForce;
    [SerializeField] private Vector3Reference _wallNormal ;
    [SerializeField] private IntReference _maxJumps ;
    #endregion

    #region Internal
    private IImpulse _motor ;
    private int _currentJumps ;
    #endregion

    #region Methods
    private void Awake() => _motor = GetComponent<IImpulse>() ;
    void OnEnable()
    {
        _isJumping.Variable.OnValueChanged += OnInputChanged;
    }
    void OnDisable()
    {
        _isJumping.Variable.OnValueChanged -= OnInputChanged;
    }

    private void OnInputChanged(bool isPressed)
    {
        if (!isPressed) return ;
        Vector3 upForce = Vector3.up * _jumpForce.Value;
        // JUMP
        if(_isGrounded.Value){
            _currentJumps = 0;
            _motor.ApplyForce(upForce) ;
        }
        // WALLJUMP
        else if (_isWallrunning.Value)
        {
            _currentJumps = 1 ;
            Vector3 horForce = _wallNormal.Value * _jumpForce.Value * 20f; // Hate the Character Controller
            _motor.ApplyForce(horForce + upForce) ;
        }
        // DOUBLEJUMP
        else if(_currentJumps < _maxJumps.Value -1)
        {
            _currentJumps++ ;
            _motor.ApplyForce(upForce * 0.8f);
        }
    }
    #endregion
}