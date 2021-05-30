using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellInfo : ScriptableObject {
    new public string name;
    public string description;
    public float cooldown;
    public float cost;
    public Sprite sprite;
    public Spell spellPrefab;
}
