using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject {
    public GameEvent hpChangeEvent;
    public GameEvent intoxicationChangeEvent;
    public GameEvent playerDeathEvent;
    // TODO: 0 HP Event?
    public float maxHp;
    public float maxIntoxication;
    public float _currentHp;
    public float CurrentHp {
        get { return _currentHp; }
        set { 
            _currentHp = value;
            if (_currentHp <= 0.0f) {
                _currentHp = 0.0f;
                playerDeathEvent.Raise();
            }
            if (hpChangeEvent == null) {
                Debug.LogError("No event found for changed intoxication level!");
            } else {
                hpChangeEvent.Raise();
            }
        }
    }
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
