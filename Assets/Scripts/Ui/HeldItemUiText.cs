using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeldItemUiText : MonoBehaviour {
    public HoldableReference heldItem;
    public Text text;

    void Awake() {
        updateHeldItem();
    }

    public void updateHeldItem() {
        if (heldItem.value == null) {
            text.text = "No Held Item";
        } else {
            text.text = "Held Item: " + heldItem.value.name;
        } 
    }
}
