#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/10
* Date: 2026-03-16
* As sources to the structure mof this class, please watch https://www.youtube.com/watch?v=raQ3iHhE_Kk for introduction
* Further sources: https://unity.com/how-to/architect-game-code-scriptable-objects, https://github.com/unity-atoms/unity-atoms, https://unity-atoms.github.io/unity-atoms/docs/
*/

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
/// This class is a ScriptableObject-based broadcast event.
/// It maintains a list of active GameEventListeners and notifies all of them when Raise() is called.
/// It must never hold reference to specific scene objects or game logic.
/// It is a pure signal. Any system that needs to respond to this event does so through a GameEventListener component.
/// </para>
/// </remarks>
/// <summary>
/// Description: A SO asset representing a single broadcast event in the game.
/// Holds a list of registered GameEventListeners and raise a signal to all of them on demand.
/// Coordination: Any MonoBehaviour can hold a reference to this asset and call Raise(). GameEventListener components register and deregister themselves automatically.
/// Deployment: Create as an asset via Assets > Create > Events > Game Event.
/// Place references to this asset on any system that needs to raise or listen to the event. 
/// </summary>
#endregion

using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventBase : ScriptableObject
{
    #region Consolidated Logic
    protected void SystemNotify(IEnumerable<GameEventListenerBase> listeners, object data)
    {
        foreach (var listener in listeners)
        {
            listener.OnEventRaised(this, data) ;
        }
    }

    protected void SystemRegister<T>(List<T> list, T listener) where T : GameEventListenerBase
    {
        if (!list.Contains(listener)) list.Add(listener) ;
    }

    protected void SystemDeregister<T>(List<T> list, T listener) where T : GameEventListenerBase
    {
        if (list.Contains(listener)) list.Remove(listener) ;
    }
    #endregion

    public abstract void RegisterListener(GameEventListenerBase listener) ;
    public abstract void DeregisterListener(GameEventListenerBase listener) ;
}

[CreateAssetMenu(fileName = "New GameEvent", menuName = "Events/Game Event")]
public class GameEvent : GameEventBase
{
    [SerializeField] private List<GameEventListener> _listeners = new();

    public void Raise() => SystemNotify(_listeners, null);

    public override void RegisterListener(GameEventListenerBase listener)
    {
        if (listener is GameEventListener specific) SystemRegister(_listeners, specific);
    }

    public override void DeregisterListener(GameEventListenerBase listener)
    {
        if (listener is GameEventListener specific) SystemDeregister(_listeners, specific);
    }
}

public abstract class GameEvent<T> : GameEventBase
{
    [SerializeField] private List<TypedGameEventListener<T, GameEvent<T>>> _listeners = new();

    public void Raise(T data) => SystemNotify(_listeners, data);

    public override void RegisterListener(GameEventListenerBase listener)
    {
        if (listener is TypedGameEventListener<T, GameEvent<T>> specific) SystemRegister(_listeners, specific);
    }

    public override void DeregisterListener(GameEventListenerBase listener)
    {
        if (listener is TypedGameEventListener<T, GameEvent<T>> specific) SystemDeregister(_listeners, specific);
    }
}