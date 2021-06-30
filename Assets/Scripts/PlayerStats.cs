using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject {
    public GameEvent intoxicationChangeEvent;
    // TODO: Separate base stats?
    public float maxHp;
    public float currentHp;
    public float maxIntoxication;
    public float _currentIntoxication;
    public float CurrentIntoxication {
        get { return _currentIntoxication; }
        set { 
            _currentIntoxication = value;
            if (intoxicationChangeEvent == null) {
                Debug.LogError("No event found for changed intoxication level!");
            } else {
                intoxicationChangeEvent.Raise();
            }
        }
    }
}
