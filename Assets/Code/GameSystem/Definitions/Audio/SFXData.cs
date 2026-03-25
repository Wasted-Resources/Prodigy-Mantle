using UnityEngine;


[CreateAssetMenu(fileName ="New SFX", menuName = "Audio/SFX Data")]
public class SFXData : SoundDataSO
{
    #region Inspector
    [Header("Variance")]
    [Range(0f,0.5f)][SerializeField] private float _volumeVariance = 0.05f ;
    [Range(0f,0.5f)][SerializeField] private float _pitchVariance = 0.1f ;
    #endregion


    #region Methods
    public override void Play(AudioSource source)
    {
        source.clip = Clip;
        source.volume = Volume + Random.Range(-_volumeVariance, _volumeVariance);
        source.pitch = Pitch + Random.Range(-_pitchVariance, _pitchVariance);
        source.loop = false;
        source.PlayOneShot(source.clip);
    }

    #endregion
}