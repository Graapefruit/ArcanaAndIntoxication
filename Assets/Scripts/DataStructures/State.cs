using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateDelegate();
public delegate State StateChangeDelegate();
public class State {
    public string name;
    public StateDelegate onEnter;
    public StateDelegate onUpdate;
    public StateDelegate onFixedUpdate;
    public StateChangeDelegate onGetNextState;
    public StateDelegate onExit;
}