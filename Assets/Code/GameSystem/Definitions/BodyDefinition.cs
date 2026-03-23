#region Project Details
using System.Net.Mime;
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/[ID]
* Date: 2026-03-23
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
/// This class handles the definition of a body/chassis. It must maintain [Architecture Constraint, e.g., Singleton].
/// Describes the the physical for of any entity body, a shared template for all entities that share a body archetype.
/// </para>
/// </remarks>
/// <summary>
/// Description: Immutable template describing a physical body archetype. Holds Locomotion stat sheet, body type classification, and prefab reference
/// Coordination: Referenced by EnemyDefinition and SuitDefinition.
/// Deployment: Create via Assets > Create > Definitions > BodyDefinition.
/// </summary>
#endregion


using UnityEngine;

[CreateAssetMenu(fileName = "New BodyDefinition", menuName = "Definitions/Body Definition")]
public class BodyDefinition : ScriptableObject, INamedAsset
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif

    [SerializeField] private string _assetName ;
    [SerializeField] private BodyType _bodyType ;
    [SerializeField] private StatSheet _statSheet ;
    [SerializeField] private GameObject _prefab ;
    //TODO: Create an assortment of Locomotive Actions for the Body Definitions to follow once Enemy AI System is figured out
    // [SerializeField] private List<LocomotiveActions> _locoActions ;
    #endregion


    #region Public Getters
    public string AssetName => _assetName;

    public TypeSO AssetType => _bodyType;
    public BodyType BodyType => _bodyType ;
    public StatSheet StatSheet => _statSheet ;
    public GameObject Prefab => _prefab ;
    // public List<LocomotiveActions> LocoActions => _locoActions ;

    #endregion
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_prefab == null) Debug.LogWarning( $"[BodyDefinition] '{name}': Prefab is not assigned.", this ) ;
        if (_statSheet == null) Debug.LogWarning( $"[BodyDefinition] '{name}': StatSheet is not assigned.", this ) ;
    }
#endif

}