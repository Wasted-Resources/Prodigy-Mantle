using UnityEngine;
/// <summary>
/// Simple Data Container for Music
/// </summary>
[CreateAssetMenu(fileName = "New MusicData", menuName = "Audio/Music Data")]
public class MusicData : SoundDataSO
{
    [SerializeField] private bool _loop = false ;

    public override void Play(AudioSource source)
    {
        source.clip = Clip;
        source.volume = Volume;
        source.pitch = Pitch;
        source.loop = _loop;
        source.Play();
    }
}