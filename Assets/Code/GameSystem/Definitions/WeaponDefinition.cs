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
/// This class handles description of weapon archetypes. its combat parameters, projectile it fires, its audio and visual feedback, classification.
/// </para>
/// </remarks>
/// <summary>
/// Description: Immutable template describing a weapon archetype. Holds combat parameters, projectile reference, audio/visual feedback and weapon classification
/// Coordination: Referenced by EnemyDefinition and SuitDefinition weapon loadout.
/// Deployment: Create via Assets > Create > Definitions > Weapon Definition.
/// </summary>
#endregion


using UnityEngine;

[CreateAssetMenu(fileName ="New WeaponDefinition", menuName = "Definitions/Weapon Definition")]
public class WeaponDefinition : ScriptableObject
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [SerializeField] private WeaponType _weaponType ;

    [Header("Combat")]
    [SerializeField] private StatSheet _weaponStats ;
    [SerializeField] private ProjectileDefinition _projectileDefinition ;
    //TODO Define CombatActions for references
    // [SerializeField] private List<CombatAction> _combatActions ;

    [Header("Feedback")]
    [SerializeField] private GameObject _prefab ;
    [SerializeField] private GameObject _vfxPrefab ;
    [SerializeField] private AudioClip _fireSound ;
    [SerializeField] private AudioClip _reloadSound ;

    #endregion


    #region Public Getters 
    public TypeSO AssetType => _weaponType;
    public WeaponType WeaponType => _weaponType ;
    public StatSheet WeaponStats => _weaponStats ;
    public GameObject Prefab => _prefab ;
    public ProjectileDefinition ProjectileDefinition => _projectileDefinition ;
   // public List<CombatAction> CombatActions => _combatActions ;
    public GameObject VFXPrefab => _vfxPrefab ;
    public AudioClip FireSound => _fireSound ;
    public AudioClip ReloadSound => _reloadSound ;
    #endregion


}