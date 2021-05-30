using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject {
    private List<EventListener> listeners = new List<EventListener>();

    public void Raise() {
        for (int i = listeners.Count-1; i >= 0; i--) {
            listeners[i].onEventRaised();
        }
    }

    public void subscribe(EventListener listener) {
        listeners.Add(listener);
    }

    public void unsubscribe(EventListener listener) {
        listeners.Remove(listener);
    }
}
