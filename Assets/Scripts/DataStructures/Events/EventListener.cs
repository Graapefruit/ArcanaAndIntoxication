using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour {
    public GameEvent gameEvent;
    public UnityEvent response;

    private void OnEnable() {
        gameEvent.subscribe(this);
    }

    private void OnDisable() {
        gameEvent.unsubscribe(this);
    }
    
    public void onEventRaised() {
        response.Invoke();
    }
}
