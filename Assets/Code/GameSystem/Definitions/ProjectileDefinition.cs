#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/16
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
/// This class handles Projectile Definitions for archetype. It must maintain combat stats, physical behaviour, lifetime, are of effect, etc.
/// </para>
/// </remarks>
/// <summary>
/// Description: Immutable template describing a projectile archetype. Holds movement parameters, lifetime, AOE, damage type and feedback params.
/// Coordination: Referenced by WeaponDefinition. Read by the projectile MonoBehaviour at spawn time. Used by ObjectPooler to maintain a pool of ready instances.
/// Deployment: Create via Assets > Create > Definitions > Projectile Definition.
/// </summary>
#endregion


using UnityEngine;

[CreateAssetMenu(fileName = "New ProjectileDefinition", menuName = "Definitions/Projectile Definition")]
public class ProjectileDefinition : ScriptableObject, INamedAsset
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [SerializeField] private string _assetName ;
    [SerializeField] private ProjectileType projectileType ;
    [SerializeField] private DamageType damageType;
    [Header("Combat")]
    [SerializeField] private StatSheet _projectileStats ;

    [Header("Feedback")]
    [SerializeField] private GameObject prefab ;
    [SerializeField] private GameObject impactVFXPrefab ;
    [SerializeField] private AudioClip impactSound ;
    #endregion


    #region Public Getters
    public string AssetName => _assetName;

    public TypeSO AssetType => projectileType;
    public DamageType DamageType => damageType ;
    public StatSheet ProjectileStats => _projectileStats ;
    public GameObject Prefab => prefab ;
    public GameObject ImpactVFXPrefab => impactVFXPrefab ;
    public AudioClip ImpactSound => impactSound ;
    #endregion

}