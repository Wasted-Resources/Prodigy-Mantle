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
/// This class handles the toplevel definition for an enemy archetype. It must maintain a BodyDefinition with enemy-specific data.
/// </para>
/// </remarks>
/// <summary>
/// Description: Top-level definition asset for an enemy archetype].
/// Coordination: Referenced by enemy prefab initialization components.
/// Deployment: Create via Assets > Create > Definitions > Enemy Definition.
/// </summary>
#endregion


using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New EnemyDefinition", menuName ="Definitions/Enemy Definition")]
public class EnemyDefinition : ScriptableObject
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
 
    [Header("Classification")]
    [SerializeField] private EnemyType _enemyType ;
    [SerializeField] private FactionType _faction ;
    [Header("Body")]
    [SerializeField] private BodyDefinition _bodyDefinition ;
    [SerializeField] private List<DamageType> _weaknesses ;
    [Header("Vitals")]
    [SerializeField] private StatSheet _vitalSheet ;

    [Header("Weapons")]
    [SerializeField] private List<WeaponDefinition> _weaponLoadout = new() ;
    #endregion


    #region Public Getters
    public TypeSO AssetType => _enemyType ;
    public EnemyType EnemyType => _enemyType ;
    public FactionType Faction => _faction ;
    public IReadOnlyList<DamageType> Weaknesses => _weaknesses ;
    public BodyDefinition BodyDefinition => _bodyDefinition ;
    public StatSheet VitalSheet => _vitalSheet ;
    public IReadOnlyList<WeaponDefinition> WeaponLoadout => _weaponLoadout ;

    #endregion


}