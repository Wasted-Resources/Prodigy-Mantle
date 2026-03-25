#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/[ID]
* Date: 2026-03-25
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
/// This class handles SoundData Definitions. It must maintain the base abstract for subclasses to inject their clip and position to the SoundManager.
/// </para>
/// </remarks>
/// <summary>
/// Description: Its a data container base for Music clips and SFX.
/// Coordination: To create new assets, please check on the subclasses.
/// Deployment: Just leave this here.
/// </summary>
#endregion


using UnityEngine;


public abstract class SoundDataSO : ScriptableObject
{
    #region Inspector
#if UNITY_EDITOR
    [TextArea] public string DeveloperDescription = string.Empty ;
#endif
    [SerializeField] private AudioClip _clip;
    [Range(0,1f)] private float _volume = 1f;
    [Range(0.5f,1.5f)] private float _pitch = 1f;
    #endregion
    #region Public Getters
    public float Volume => _volume ;
    public float Pitch => _pitch ;
    public AudioClip Clip => _clip ;
    #endregion

    
    #region Methods
    public abstract void Play(AudioSource source) ;
    #endregion
}