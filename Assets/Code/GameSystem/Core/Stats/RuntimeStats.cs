#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/14
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
/// This class handles the per-instance live stat container for a single entity. It is initialized from StatSheet at Awake.
/// StatSheet holds the Reference lists pointing at Variable assets. RuntimeStats iterates those lists at Initialize(), 
/// reads Variable.name as ker and Variable.ClampedInitialValue as value, and stores them in typed dictionaries.
/// </para>
/// </remarks>
/// <summary>
/// Description: Per-instance live stat container. Dictionary-backed. Initialized by iterating StatSheet Reference lists at Awake.
/// Coordination: Owned by behaviour components.
/// Deployment: Instantiated as a private field on a MonoBehaviour.
/// </summary>
#endregion

using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RuntimeStats : MonoBehaviour
{
    #region Internal
    private readonly Dictionary<ScriptableObject, object> _instances = new() ;
    #endregion


    #region Public Getters
    public IReadOnlyDictionary<ScriptableObject, object> Instances => _instances;

    #endregion
    
    #region Methods
    public void Initialize(StatSheet sheet)
    {
        _instances.Clear();
        //TODO WHYYYYY????
        RegisterStats(sheet.FloatStats.Cast<BaseReference>());
        RegisterStats(sheet.BoolStats.Cast<BaseReference>());
    }

    private void RegisterStats(IEnumerable<BaseReference> refs)
    {
        foreach (var rf in refs)
        {
            var asset = rf.GetVariableAsset();
            if (asset is IInstanceProvider provider) _instances[asset] = provider.CreateInstance();
        }
    }
    public void Set<T>(Variable<T> asset, T newValue)
    {
        if (_instances.TryGetValue(asset, out object instance))
            ((StatInstance<T>)instance).SetValue(newValue);
        else
            asset.Value = newValue ;
    }
    public T Get<T>(Variable<T> asset)
    {
        if (_instances.TryGetValue(asset, out object instance))
            return ((StatInstance<T>)instance).Value ;
        return asset.Value ;
    }
    #endregion
}