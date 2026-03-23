#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/15
* Date: 2026-03-20
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
/// It describes a single stat type at project level. AssetName must exactly match the corresponding StatNames constant.
/// AssetType is null for stat definitions.
/// </para>
/// <para>
/// Adding a new stat:
/// 1. Add const string to StatNames.cs
/// 2. Create a StatDefinition asset — set AssetName to match the constant
/// 3. Add to StatRegistry asset
/// 4. Add entries to relevant StatSheet assets
/// </para>
/// </remarks>
/// <summary>
/// Description: Describes a single stat type - name, default value, valid range and optional flag.
/// Coordination: Held in StatRegistry. Read by RuntimeStats.Initialize() to determine defaults, clamp boundaries and optional initialization behaviour.
/// Deployment: Create via Assets > Create > Definitions > Stat Definition.
/// </summary>
#endregion


using UnityEngine;

[CreateAssetMenu(fileName = "New StatDefinition", menuName = "Definitions/Stat Definition")]
public class StatDefinition : ScriptableObject, IHaveTypes
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [SerializeField] private string _statName ;
    [SerializeField] private float _defaultValue ;
    [SerializeField] private float _minValue = 0;
    [SerializeField] private float _maxValue = 9999;
    [SerializeField] private bool _optional = true ;
    #endregion

    #region Public Getters
    public float DefaultValue => _defaultValue ;
    public float MinVale => _minValue ;
    public float MaxValue => _maxValue ;
    public bool Optional => _optional ;
    public string AssetName => _statName ;

    public TypeSO AssetType => null ;


    #endregion
}