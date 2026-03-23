using UnityEngine;
/// <summary>
/// Pure Signal event. Passes through empty data struct. No payload needed for those.
/// Deprecated.
/// </summary>
[CreateAssetMenu(fileName ="new GOEvent", menuName ="Events/GameObject Event")]
public class GameObjectEvent : GameEvent<GameObject> { }