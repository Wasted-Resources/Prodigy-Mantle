#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/10
* Date: 2026-03-16
* As sources to the structure mof this class, please watch https://www.youtube.com/watch?v=raQ3iHhE_Kk for introduction
* Further sources: https://unity.com/how-to/architect-game-code-scriptable-objects, https://github.com/unity-atoms/unity-atoms, https://unity-atoms.github.io/unity-atoms/docs/
*/
#endregion


#region Basic Instructions
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
/// This MonoBehaviour bridges a GameEvent asset and a UnityEvent response. It registers itself with  the referenced GameEvent on Enable and de-registers itself OnDisable, 
/// so the listener is always in sync with the active state of the GameObject it lives on.
/// It must not contain game logic itself.
/// This class has been expanded and contains now the two bases for the two types of GameEvents, signals and typed GameEvents.
/// GameEventListener is a concrete signal listener. While TypedGameEventListener has concrete subclasses to implement, according to type.
/// </para>
/// </remarks>
/// <summary>
/// Description: It listens to a single GameEvent asset and invokes a UnityEvent response when that event is raised.
/// Coordination: Assign a GameEvent asset in the Inspector. Wire the desired response methods into the Response UnityEvent field. The component handles registration and de-registration automatically.
/// GameEventListeners for signals can have a list of event-response pairs. TypedGameEventListener can only pair a single response to an event, so if you need more than one, use multiple listeners.
/// Deployment: Add as a component to any GameObject in the scene that needs to respond to a GameEvent. Does not need to be the same GameObject as the system that raises the event.
/// </summary>
#endregion


using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("EventListener")]
public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _event;
    [SerializeField] private UnityEvent _response;

    private void OnEnable() => _event?.RegisterListener(this);
    private void OnDisable() => _event?.DeregisterListener(this);

    public void OnEventRaised()
    {
        _response?.Invoke();
    }
}