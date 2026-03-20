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
/// This class handles [Core Responsibility]. It must maintain [Architecture Constraint, e.g., Singleton].
/// </para>
/// </remarks>
/// <summary>
/// Description: [Describe what this class does].
/// Coordination: [How it communicates with APIs or other Components].
/// Deployment: [Where it should live in the Scene, Project, Assets'].
/// </summary>
#endregion



public static class StatNames
{
    #region Vitals
    public const string MaxHealth = "MaxHealth" ;
    public const string Health = "Health" ;
    public const string MaxShield = "MaxShield" ;
    public const string Shield = "Shield" ;
    public const string Defense = "Defense" ;
    public const string MaxEnergy = "MaxEnergy" ;
    public const string Energy = "Energy" ;
    public const string MaxSR = "MaxSpecialResource" ;
    public const string SR = "SpecialResource" ;
    #endregion


    #region Locomotion
    public const string MoveSpeed = "MoveSpeed" ;
    public const string Acceleration = "Acceleration" ;
    public const string TurnSpeed = "TurnSpeed" ;
    #endregion


    #region Combat
    public const string BaseDamage = "BaseDamage" ;
    public const string EffectiveRange = "EffectiveRange" ;
    public const string EffectRadius = "EffectRadius" ;
    public const string DetectionRange = "DetectionRange" ;
    public const string EngagementRange = "EngagementRange" ;

    #endregion
}