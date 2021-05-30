using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {
    public Player caster;
    public Vector3 location;
    public float intoxicationLevel = -1;
    protected abstract void Update();
    protected void throwIfNotAllNecessaryDataPresent() {
        if (caster == null || location == null || intoxicationLevel < 0) {
            Debug.LogErrorFormat("Spell created with insufficient data:\nCaster: {0}\nLocation: {1}\nIntoxication Level: {2}", caster, location, intoxicationLevel, transform);
        }
    }
}
