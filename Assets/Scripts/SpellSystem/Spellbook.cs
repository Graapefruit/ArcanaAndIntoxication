using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Spellbook : ScriptableObject {
    [SerializeField]
    private List<SpellInfo> spells;
    [SerializeField]
    // The fact that spells are indexed here as 0-3 but we use hotkeys 1-4 makes the below code have a lot of annoying off-by-one issues that should remain fixed
    private int[] hotbarToSpellbook;
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
        if (hotbarToSpellbook[h] != -1) {
            spellbookToHotbar[hotbarToSpellbook[h]] = 0;
        }
        hotbarToSpellbook[h-1] = i;
        spellbookToHotbar[i] = h;
    }

    public int getSpellCount() {
        return spells.Count;
    }
}
