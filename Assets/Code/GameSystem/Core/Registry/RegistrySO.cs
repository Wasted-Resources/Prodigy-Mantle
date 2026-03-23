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
/// This file defines two two generic registry base classes:
/// Registry<T> is the base for any project-wide based lookup, typed filtering and randomized selection. T must be a ScriptableObject.
/// NameRegistry<T> is an extension for assets that implement INamedAsset.
/// </para>
/// <para>
/// Concrete registries are one-line subclasses
/// public class StatRegistry : NamedRegistry<StatDefinition> {} for example.
/// </para>
/// </remarks>
/// <summary>
/// Description: Generic ScriptableObject registry base classes.
/// Coordination: Concrete registries referenced by spawners, initialization systems, and any component that needs to query a pool of definition assets.
/// Deployment: Concrete subclasses are assets.
/// </summary>
#endregion


using System.Collections.Generic ;
using UnityEditor;
using UnityEngine;


public abstract class Registry<T> : ScriptableObject where T : ScriptableObject
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [SerializeField] private List<T> _entries = new() ;
    #endregion
    

    #region Public Getters
    public IReadOnlyList<T> AllEntries => _entries ;

    public T Get(System.Func<T, bool> predicate)
    {
        foreach (var entry in _entries)
            if (entry != null && predicate(entry)) return entry ;
        return null ;
    }
    public List<T> GetAll(System.Func<T,bool> predicate)
    {
        var results = new List<T>() ;
        foreach (var entry in _entries)
            if (entry != null && predicate(entry))
                results.Add(entry) ;
        return results ;
    }
    #endregion
    
    #region Methods
    /// <summary>
    /// Brief description of the method.
    /// </summary>
    /// <param name = "parameters">What this parameter represents </param>
    public List<T> GetAllOfType(TypeSO type)
    {
        var results = new List<T>() ;
        foreach (var entry in _entries)
        {
            if (entry == null) continue ;
            if (entry is INamedAsset named && named.AssetType == type)
                results.Add(entry) ;
        }
        return results ;
    }
    public T GetRandom(TypeSO type)
    {
        var matches = GetAllOfType(type) ;
        return matches[Random.Range(0, matches.Count)] ;
    }
    public T GetRandom()
    {
        return _entries[Random.Range(0,_entries.Count)] ;
    }

    #endregion


    #region Automation
#if UNITY_EDITOR
    [Header("Automation")]
    [SerializeField] private DefaultAsset _searchFolder ;

    [ContextMenu("Populate Registry From Folder")]
    public void PopulateFromFolder()
    {
        string path = AssetDatabase.GetAssetPath(_searchFolder);
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] {path}) ;
        _entries.Clear();
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid) ;
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if(asset != null) _entries.Add(asset) ;
        }
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
    #endregion
}
public abstract class NamedRegistry<T> : Registry<T> where T : ScriptableObject, INamedAsset
{
    public T GetByName(string assetName) => Get(entry => entry.AssetName == assetName) ;
}