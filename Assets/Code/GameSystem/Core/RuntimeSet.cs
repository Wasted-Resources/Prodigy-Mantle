#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/14
* Date: 2026-03-19
* Source: Ryan Hipple, Unite Austin 2017 - Game Architecture with Scriptable Objects
* Original: https://github.com/roboryantron/Unite2017/blob/master/Assets/Code/Sets/RuntimeSet.cs
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
/// This is the abstract generic base class for all RuntimeSet assets. A RuntimeSet is a SO that holds a live collection of objects at runtime. Objects register themselves OnEnable and de-register themselves OnDisable automatically.
/// A similar approach as the GameEventListener component.
/// </para>
/// <para>
/// It replaces FindObjectsOfType and scene-bound manager lists entirely. Any system that needs to query what exists right now reads from a RunTimeSet instead of holding direct references or searching the scene hierarchy.
/// </para>
/// </remarks>
/// <summary>
/// Description: Abstract generic base for SO-based runtime collections. Holds a live list of objects, fires change events and resets cleanly on domain reload.
/// Coordination: Objects add themselves OnEnable and OnDisable. Listening systems subscribe to OnItemAdded/Removed instead of polling.
/// Deployment: Not instantiated directly. Concrete subclasses are assets.
/// </summary>
#endregion


using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class RuntimeSet<T> : ScriptableObject, ISerializationCallbackReceiver
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    
    #endregion


    #region Internal
    /// <summary>
    /// Private list. all mutations go through Add() and Remove() to preserve integrity guards and ensure change events are always fired correctly.
    /// </summary>
    private readonly List<T> _items = new() ;

    #endregion
    

    #region Public Getters
    /// <summary>
    /// Read-only view of the live collection.
    /// </summary>
    public IReadOnlyList<T> Items => _items ;

    public event Action<T> OnItemAdded ;

    public event Action<T> OnItemRemoved ;

    #endregion


    #region Methods

    /// <summary>
    /// Adds an item to the set if it is not already present. Fires OnItemAdded if the item was successfully added.
    /// </summary>
    /// <param name="item">Item to register to this set.</param>
    public void Add(T item)
    {
        if (!_items.Contains(item))
        {
            _items.Add(item) ;
            OnItemAdded?.Invoke(item) ;
        }
    }

    /// <summary>
    /// Removes an item from the set if it is present. Fires OnItemRemoved if the item was successfully removed.
    /// </summary>
    /// <param name="item">Item to de-register from this set</param>
    public void Remove(T item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item) ;
            OnItemRemoved?.Invoke(item) ;
        }
    }

    /// <summary>
    /// Returns true if the set currently contains the given item.
    /// </summary>
    /// <param name="item">The item to check for membership.</param>
    /// <returns></returns>
    public bool Contains(T item) => _items.Contains(item) ;

    /// <summary>
    /// In the Editor this prevents stale Play-mode references accumulating between sessions. In a build this fires once at asset load time when the list is already empty, making it safe.
    /// </summary>
    public void OnAfterDeserialize() => _items.Clear() ;

    public void OnBeforeSerialize() {    }  // Do nothing, for now. Maybe i will Load it from the SaveManager i am not sure about the structure yet.

    #endregion
}

#region RuntimeSet Definitions

// Enemy Set
/// <summary>
/// Holds all active enemy entities currently alive in the scene. Used by systems that needs to query or interact with live enemy entities.
/// </summary>
[CreateAssetMenu(fileName ="New EnemySet", menuName ="Runtime/Enemy Set")]
public class EnemySet : RuntimeSet<GameObject> { }

// Projectile Set
/// <summary>
/// Holds all active projectile in flight. Used by object poolers and any system that needs to query or interact with live projectiles.
/// </summary>
[CreateAssetMenu(fileName ="New ProjectileSet", menuName ="Runtime/Projectile Set")]
public class ProjectileSet : RuntimeSet<GameObject> { }

// Spawn Point Set
/// <summary>
/// Holds all available Spawn Points for enemies. Used by object poolers and any system that needs to interact with spawn points.
/// </summary>
[CreateAssetMenu(fileName ="New SpawnPoints", menuName ="Runtime/SpawnPoints Set")]
public class SpawnPoints : RuntimeSet<GameObject> { }

// For further Runtime Set Definitions follow the patterns as shown above.

#endregion