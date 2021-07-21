using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager {
    private string name;
    private State currentState;

    public StateManager(string name, State startingState) {
        this.currentState = startingState;
    }

    public void doFixedUpdate() {
        doStateTransitionIfNeeded();
        doIfExists(currentState.onFixedUpdate);
    }

    public void doUpdate() {
        doStateTransitionIfNeeded();
        doIfExists(currentState.onUpdate);
    }

    private void doStateTransitionIfNeeded() {
        State nextState;
        nextState = doWithErrorHandling(currentState.onGetNextState, "Get Next State", currentState.name);
        if (nextState != null && nextState != this.currentState) {
            doIfExists(currentState.onExit);
            this.currentState = nextState;
            doIfExists(currentState.onEnter);
        }
    }

    private void doWithErrorHandling(StateDelegate f, string stateMethodName, string stateName) {
        if (f != null) {
            f();
        } else {
            Debug.LogErrorFormat("Error: State Manager \"{0}\" has no \"{1}\" set for state \"{2}\"!", name, stateMethodName, stateName);
        }
    }

    private State doWithErrorHandling(StateChangeDelegate f, string stateMethodName, string stateName) {
        if (f != null) {
            return f();
        } else {
            Debug.LogErrorFormat("Error: State Manager \"{0}\" has no \"{1}\" set for state \"{2}\"!", name, stateMethodName, stateName);
            return null;
        }
    }

    private void doIfExists(StateDelegate f) {
        if (f != null) {
            f();
        }
    }
}
