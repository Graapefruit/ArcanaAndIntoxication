using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellbookHotbarImage : MonoBehaviour {
    public Spellbook spellbook;
    public int correspondingIndex;
    private Image image;
    void Start() {
        image = transform.GetComponent<Image>();
    }

    // TODO: Event System!
    void Update() {
        if (correspondingIndex <= 4 && correspondingIndex > 0) {
            SpellInfo spellInfo = spellbook.getSpellFromHotbar(correspondingIndex);
            if (spellInfo == null) {
                image.sprite = null;
                image.enabled = false;
            } else {
                image.sprite = spellInfo.sprite;
                image.enabled = true;
            }
        } else {
            Debug.LogErrorFormat("Spellbook Hotbar Image does not have a proper index associated with it: {0}", correspondingIndex, transform);
            image.sprite = null;
            image.enabled = false;
        }
    }
}
