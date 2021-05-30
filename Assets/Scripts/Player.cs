using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public PlayerStats stats;
    public Inventory inventory;
    public Spellbook spellbook;

    public void castSpell(int hotkey, Vector3 castLocation) {
        SpellInfo spellInfo = spellbook.getSpellFromHotbar(hotkey);
        if (spellInfo != null) {
            Spell spellPrefab = spellInfo.spellPrefab;
            Spell spellInstance = Instantiate(spellPrefab, transform.position, Quaternion.identity);
            spellInstance.caster = this;
            spellInstance.intoxicationLevel = 0;
            spellInstance.location = castLocation;
        }
    }
}
