using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu]
public class Spellbook : ScriptableObject {
    [SerializeField]
    private List<SpellInfo> spells;
    [SerializeField]
    // The fact that spells are indexed here as 0-3 but we use hotkeys 1-4 makes the below code have a lot of annoying off-by-one issues that should remain fixed
    private int[] hotbarToSpellbook;
    private float[] timeLastCast;
    [SerializeField]
    public GameEvent hotbarUpdateEvent;
    private List<int> spellbookToHotbar;

    void OnValidate() {
        spellbookToHotbar = new List<int>();
        for (int i = 0; i < spells.Count; i++) {
            spellbookToHotbar.Add(0);
        }
        for (int i = 0; i < 4; i++) {
            if (hotbarToSpellbook[i] > -1) {
                spellbookToHotbar[hotbarToSpellbook[i]] = i+1;
            }
        }
        timeLastCast = new float[4];
        for (int i = 0; i < 4; i++) {
            timeLastCast[i] = 0.0f;
        }
    }

    public SpellInfo getSpell(int i) {
        return spells[i];
    }

    public SpellInfo getSpellFromHotbar(int h) {
        h = h - 1;
        int index = hotbarToSpellbook[h];
        if (index == -1) {
            return null;
        } else {
            return spells[hotbarToSpellbook[h]];
        }
    }

    public int getSpellsHotbarMapping(int i) {
        return spellbookToHotbar[i];
    }

    // public void addNewSpell(SpellInfo spellInfo) {
        
    // }

    public void setNewHotbarMapping(int h, int i) {
        if (hotbarToSpellbook[h-1] != -1) {
            spellbookToHotbar[hotbarToSpellbook[h-1]] = 0;
        }
        if (spellbookToHotbar[i] != 0) {
            hotbarToSpellbook[spellbookToHotbar[i]-1] = -1;
        }
        hotbarToSpellbook[h-1] = i;
        spellbookToHotbar[i] = h;
        hotbarUpdateEvent.Raise();
    }

    public int getSpellCount() {
        return spells.Count;
    }

    public bool spellOffCooldown(int h) {
        return Time.time - timeLastCast[h-1] >= getSpellFromHotbar(h).cooldown;
    }

    public float updateAndGetRemainingCooldownPercentage(int h) {
        return Mathf.Max(0, Time.time - timeLastCast[h-1]);
    }

    public void putSpellOnCooldown(int h) {
        timeLastCast[h-1] = Time.time;
    }
}
