#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/12
* Date: 2026-03-18
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
/// This file defines the abstract Type base and all concrete type assets used across the game system. TypeSO replaces plain enums wherever applicable, meaning wherever it needs to be configurable in the Inspector, extensible by design, etc.
/// </para>
/// </remarks>
/// <summary>
/// Description: Abstract base for all SO-based types.
/// Concrete subclasses act as individual enum values, represented as assets.
/// Coordination: Assign concrete type assets to fields in MonoBehaviour Scripts or Scriptable Objects. Compare by reference equality.
/// Deployment: Concrete subclasses are assets created via Assets > Create > Types > [Type].
/// The base class itself is never instantiated directly.
/// </summary>
#endregion


using UnityEngine;


public abstract class TypeSO : ScriptableObject
{
    #region Inspector 
#if UNITY_EDITOR
    [TextArea]
    public string DeveloperDescription = string.Empty ;
#endif
    #endregion
}

#region Type Definitions
// Game State
[CreateAssetMenu(fileName ="New GameState", menuName ="Types/GameState")]
public class GameState : TypeSO { }

// Entity State
[CreateAssetMenu(fileName ="New EntityState", menuName ="Types/EntityState")]
public class EntityState : TypeSO { }

// Faction
[CreateAssetMenu(fileName ="New FactionType", menuName ="Types/Faction")]
public class FactionType : TypeSO { }

// Body Base
[CreateAssetMenu(fileName ="New BodyType", menuName ="Types/BodyType")]
public class BodyType : TypeSO { }

// Weapon Type
[CreateAssetMenu(fileName ="New WeaponType", menuName ="Types/WeaponType")]
public class WeaponType : TypeSO { }

// Damage Type
[CreateAssetMenu(fileName ="New DamageType", menuName ="Types/DamageType")]
public class DamageType : TypeSO { }

// EnemyType
[CreateAssetMenu(fileName ="New EnemyType", menuName ="Types/EnemyType")]
public class EnemyType : TypeSO { }

#endregion
