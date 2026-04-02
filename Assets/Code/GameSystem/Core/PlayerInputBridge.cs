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
using UnityEngine.InputSystem;

public class PlayerInputBridge : MonoBehaviour
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [Header("Input Data Variables")]
    [SerializeField] private Vector2Variable _moveInput ;
    [SerializeField] private Vector3Variable _cameraForward ;

    [Header("State Data Variables")]
    [SerializeField] private BoolVariable _isGrounded;
    [SerializeField] private BoolVariable _isAirbourne;
    [SerializeField] private BoolVariable _isWallrunning;
    [SerializeField] private BoolVariable _isJumping;
    [SerializeField] private BoolVariable _isDashing;
    [SerializeField] private BoolVariable _useGravity;

    [Header("Events")]
    [SerializeField] private GameObjectEvent _moveEvent;
    [SerializeField] private GameObjectEvent _jumpEvent;
    [SerializeField] private GameObjectEvent _dashEvent;
    [SerializeField] private WeaponEvent _primaryEvent;
    [SerializeField] private WeaponEvent _secondaryEvent;
    [SerializeField] private WeaponEvent _specialEvent;
    [SerializeField] private WeaponEvent _reloadEvent;
    [SerializeField] private GameEvent _prevEvent;
    [SerializeField] private GameEvent _nextEvent;
    [SerializeField] private GameObjectEvent _interactEvent;
    [SerializeField] private GameEvent _menuEvent;

    #endregion
    #region Internal
    private PlayerControls _controls;
    /// <summary>
    /// Subscription service would be nice
    /// </summary>
    private void Awake()
    {
        _controls = new();
        // MOVEMENT
        _controls.Player.Move.performed += ctx => {
            _moveInput.Value = ctx.ReadValue<Vector2>();
            _moveEvent?.Raise(gameObject);
        };
        _controls.Player.Move.canceled += ctx => {
            _moveInput.Value = Vector2.zero;
            _moveEvent?.Raise(gameObject);
        };

        // JUMP
        _controls.Player.Jump.started += ctx => {
            _isJumping.Value = true;
            _jumpEvent?.Raise(gameObject);
        };
        _controls.Player.Jump.canceled += ctx => _isJumping.Value = false;

        // DASH
        _controls.Player.Dash.started += ctx => {
            _isDashing.Value = true;
            _dashEvent?.Raise(gameObject);
        };
        _controls.Player.Dash.canceled += ctx => _isDashing.Value = false;

        // ONE-SHOT SIGNALS
        _controls.Player.Primary.started += _ => _primaryEvent?.Raise(gameObject);
        _controls.Player.Secondary.started += _ => _secondaryEvent?.Raise(gameObject);
        _controls.Player.Interact.started += _ => _interactEvent?.Raise(gameObject);
        _controls.Player.Reload.started += _ => _reloadEvent?.Raise(gameObject);
        _controls.Player.Special.started += _ => _specialEvent?.Raise(gameObject);
        _controls.Player.Next.started += _ => _nextEvent?.Raise();
        _controls.Player.Previous.started += _ => _prevEvent?.Raise();
        _controls.Player.Menu.started += _ => _menuEvent?.Raise();
    }

    private void OnEnable() => _controls.Enable();
    private void OnDisable() => _controls.Disable();
    
    #endregion

}