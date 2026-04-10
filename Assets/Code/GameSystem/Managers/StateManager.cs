#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/[ID]
* Date: 2026-04-09
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


public class StateManager : MonoBehaviour
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [Header("Layer Settings")]
    [SerializeField] private LayerMask _groundLayer ;
    [SerializeField] private LayerMask _wallLayer ;

    [Header("Detection Settings")]
    [SerializeField] private float _shellOffset = 0.05f ;

    [Header("State Variables")]
    [SerializeField] private BoolVariable _isGrounded ;
    [SerializeField] private BoolVariable _isAirbourne ;
    [SerializeField] private BoolVariable _isWallrunning ;
    [SerializeField] private Vector3Variable _wallNormal ;
    #endregion

    #region Internal
    private Collider _collider ;
    private float _wallTouchGraceTime = 0.2f ; // Coyote time apparently, funny concept but again. i wouldnt have that issue with a rigidbody!!
    private float _wallTouchTimer ;


    void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    #endregion

    #region Methods
    void FixedUpdate()
    {
        Bounds b = _collider.bounds ;
        float radius = b.extents.x ;
        Vector3 checkPos = new(b.center.x, b.min.y, b.center.z) ;
        Vector3 sphereCenter = checkPos + Vector3.up * radius;
        
        bool grounded = Physics.CheckSphere(sphereCenter, radius + _shellOffset, _groundLayer) ;
        _isGrounded.Value = grounded ;
        _isAirbourne.Value = !grounded ;

        if (gameObject.CompareTag("Player") && !grounded)
        {
            WallDetection(sphereCenter, radius);
        }
        else _isWallrunning.Value = false ;

        Debug.Log($"[StateManager] Grounded: {grounded} | Layer Check: {LayerMask.LayerToName(gameObject.layer)}");
    }

    private void WallDetection(Vector3 center, float radius)
    {
        float detectionRange = _shellOffset * 5f ;

        if (Physics.SphereCast(center, radius, transform.forward, out var hit, detectionRange, _wallLayer) ||
        Physics.SphereCast(center, radius, -transform.right, out hit, detectionRange, _wallLayer) ||
        Physics.SphereCast(center, radius, transform.right, out hit, detectionRange, _wallLayer) ||
        Physics.SphereCast(center, radius, -transform.forward, out hit, detectionRange, _wallLayer))
        if (Vector3.Angle(hit.normal, Vector3.up) > 45f) 
        {
            _isWallrunning.Value = true;
            _wallNormal.Value = hit.normal;
        }
        
        _isWallrunning.Value = (Time.time - _wallTouchTimer) < _wallTouchGraceTime ; // SO convoluted. this is to fix my race condition between this one and the JumpHandler
        
    }
    #endregion
}